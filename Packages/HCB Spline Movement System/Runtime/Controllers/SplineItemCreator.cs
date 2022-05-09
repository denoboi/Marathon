using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HCB.SplineMovementSystem
{
    public class SplineItemCreator : MonoBehaviour
    {
        public List<SplineItemPrefab> SplineItemPrefabs;      
    }

    [System.Serializable]
    public struct SplineItemPrefab
    {
#if UNITY_EDITOR
        public GameObject ItemPrefab;
        public SplineComputer Spline;

        [Space(10)]
        [Header("Spawn Options")]
        public int ItemAmount;

        [Tooltip("Spacing between items")]
        public float Spacing;

        [Tooltip("Offset to spline")]
        public Vector2 Offset;

        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [Button(ButtonSizes.Large)]        
        public void Create()
        {
            if (Spline == null)
            {
                Debug.LogError("Please Attach the Spline");
                return;
            }

            Transform itemParent = null;

            if (ItemAmount > 1)
            {
                GameObject itemParentGo = new GameObject(ItemPrefab.name + "'s " + "Parent");

                itemParentGo.transform.position = Vector3.zero;
                itemParent = itemParentGo.transform;

                itemParentGo.AddComponent<SplineItem>().Initialize(Offset, 0, Spline);
            }

            for (int i = 0; i < ItemAmount; i++)
            {
                GameObject item = PrefabUtility.InstantiatePrefab(ItemPrefab) as GameObject;

                if (item == null) 
                {                    
                    Debug.LogError("This object is not a prefab! Attach prefab from project folder");                    
                    return;
                }

                item.transform.SetParent(itemParent);

                float distanceFromStart = Spacing * i;
                CheckSplineItemComponent(item, distanceFromStart);
            }
        }

        private void CheckSplineItemComponent(GameObject item, float distanceFromStart)
        {
            if (!item.TryGetComponent(out SplineItem splineItem))
            {
                splineItem = item.AddComponent<SplineItem>();
            }

            splineItem.Initialize(Offset, distanceFromStart, Spline);
        }
#endif
    }
}
