using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using HCB.Core;
using HCB.SplineMovementSystem;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject[] wallPieces;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        foreach (var wall in wallPieces)
        {
            wall.AddComponent<Rigidbody>().isKinematic = true;
            wall.AddComponent<MeshCollider>().convex = true;
        }
    }

    [Button]
    private void DestructWall()
    {
        foreach (var wall in wallPieces)
        {
            wall.GetComponent<Rigidbody>().isKinematic = false;
            wall.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2)) * 200);
        }
    }

    private bool isCollided;

    private void OnTriggerEnter(Collider other)
    {
        

        Ragdoll player = other.GetComponent<Ragdoll>();

        

        if (player != null && !isCollided)
        {
            isCollided = true;
            Debug.Log(other.name);
            //StackController sc = splineCharacter.GetComponent<StackController>();

            //if (sc == null) return;

            DestructWall();

            //if (sc.CurrentCollectibles.Count == 0)
            //{
            //    GameManager.Instance.CompeleteStage(false);
            //    return;
            //}

            //sc.CurrentCollectibles[sc.CurrentCollectibles.Count - 1].Uncollect(sc);
            HapticManager.Haptic(HapticTypes.RigidImpact);
        }
    }
}