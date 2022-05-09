using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using UnityEditor.SceneManagement;
#endif

namespace HCB.SplineMovementSystem.SplineEditor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SplineItem)), CanEditMultipleObjects]
    public class SplineItemEditor : OdinEditor
    {
        private const float SIZE_MULTIPLIER = 0.13f;
        private const float SNAP = 0.01f;

        private Tool _lastTool = Tool.None;
        private float _size = 1;

        private SplineItem[] _splineItems;
        private SplineItem[] _parentSplineItems;

        protected override void OnEnable()
        {
            base.OnEnable();

            _splineItems = GetSplineItems();
            _parentSplineItems = GetParentSplineItems();

            _lastTool = Tools.current;
            Tools.current = Tool.None;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Tools.current = _lastTool;
        }

        protected virtual void OnSceneGUI()
        {
            SplineItem selectedItem = _splineItems[0];
            SplineItem splineItem = (SplineItem)target;

            if (splineItem != selectedItem)
                return;

            _size = Vector3.Distance(SceneView.lastActiveSceneView.camera.transform.position, selectedItem.transform.position) * SIZE_MULTIPLIER;

            CheckXAxis(selectedItem);
            CheckYAxis(selectedItem);
            CheckZAxis(selectedItem);           
        }

        private void CheckXAxis(SplineItem selectedItem)
        {
            Transform transform = selectedItem.transform;

            EditorGUI.BeginChangeCheck();

            Handles.color = Handles.xAxisColor;
            Vector3 newPosition = Handles.Slider(transform.position, transform.right, _size, Handles.ArrowHandleCap, SNAP);
            Vector3 diff = (newPosition - transform.position);

            if (EditorGUI.EndChangeCheck())
            {
                foreach (var splineItem in _parentSplineItems)
                {
                    splineItem.transform.position += diff;
                }

                foreach (var splineItem in _splineItems)
                {
                    splineItem.SetOffset();
                }

                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }

        private void CheckYAxis(SplineItem selectedItem)
        {
            Transform transform = selectedItem.transform;

            EditorGUI.BeginChangeCheck();

            Handles.color = Handles.yAxisColor;
            Vector3 newPosition = Handles.Slider(transform.position, transform.up, _size, Handles.ArrowHandleCap, SNAP);
            Vector3 diff = (newPosition - transform.position);

            if (EditorGUI.EndChangeCheck())
            {
                foreach (var splineItem in _parentSplineItems)
                {
                    splineItem.transform.position += diff;
                }

                foreach (var splineItem in _splineItems)
                {
                    splineItem.SetOffset();
                }

                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }

        private void CheckZAxis(SplineItem selectedItem)
        {
            Transform transform = selectedItem.transform;

            EditorGUI.BeginChangeCheck();

            Handles.color = Handles.zAxisColor;
            Vector3 newPosition = Handles.Slider(transform.position, transform.forward, _size, Handles.ArrowHandleCap, SNAP);

            if (EditorGUI.EndChangeCheck())
            {
                double handlePercent = selectedItem.GetPercent(newPosition);
                float difference = 0f;
                bool isForward = false;

                if (selectedItem.SplinePercent < handlePercent)
                {
                    difference = selectedItem.SplineComputer.CalculateLength(selectedItem.SplinePercent, handlePercent);
                    isForward = true;
                }

                else
                {
                    difference = selectedItem.SplineComputer.CalculateLength(handlePercent, selectedItem.SplinePercent);
                    isForward = false;
                }

                foreach (var splineItem in _splineItems)
                {
                    splineItem.Travel(difference, isForward);
                }

                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }

        private SplineItem[] GetSplineItems()
        {
            List<SplineItem> splineItems = new List<SplineItem>(targets.Cast<SplineItem>().ToArray());

            for (int i = 0; i < splineItems.Count; i++)
            {
                SplineItem[] childSplineItems = splineItems[i].GetComponentsInChildren<SplineItem>();

                foreach (var childSplineItem in childSplineItems)
                {
                    if (!splineItems.Contains(childSplineItem))
                    {
                        splineItems.Add(childSplineItem);
                    }
                }
            }

            return splineItems.ToArray();
        }

        private SplineItem[] GetParentSplineItems()
        {
            List<SplineItem> parentSplineItems = new List<SplineItem>(targets.Cast<SplineItem>().ToArray());
            List<SplineItem> parentSplineItemsTemp = new List<SplineItem>(parentSplineItems);

            for (int i = 0; i < parentSplineItems.Count; i++)
            {
                SplineItem splineItem = parentSplineItems[i].GetComponentInParent<SplineItem>();
                if (splineItem != parentSplineItems[i])
                {
                    parentSplineItemsTemp.Remove(parentSplineItems[i]);
                }
            }

            return parentSplineItemsTemp.ToArray();
        }
    }
#endif
}
