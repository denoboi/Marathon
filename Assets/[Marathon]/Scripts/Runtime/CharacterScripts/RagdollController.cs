using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;



[TypeInfoBox("Don't forget to disable physics collision layers")]
public class RagdollController : MonoBehaviour
{
    public int CharacterLayerIndex;
    public int RagdollLayerIndex;

    public List<Ragdoll> Ragdolls = new List<Ragdoll>();

    List<Animator> animators;
    List<Animator> Animators { get { return (animators == null || animators.Count == 0) ? animators = new List<Animator>(GetComponentsInChildren<Animator>(true)) : animators; } }

    private Collider col;
    public Collider Col { get { return col == null ? col = GetComponent<Collider>() : col; } }

    private Rigidbody rigidbody;
    public Rigidbody Rigidbody { get { return rigidbody == null ? rigidbody = GetComponent<Rigidbody>() : rigidbody; } }

    private void Awake()
    {
        InitializeRagdolls();
    }

    private void InitializeRagdolls()
    {
        var ragdolls = GetComponentsInChildren<Ragdoll>();

        foreach (var ragdoll in ragdolls)
        {
            Ragdolls.Add(ragdoll);
        }
    }

    [Button]
    public void EnableRagdollWithForce(Vector3 forceAxis, float forceAmount)
    {
        Ragdoll[] ragdolls = GetComponentsInChildren<Ragdoll>();

        if (Col != null)
            Col.isTrigger = true;

        if (Rigidbody != null)
            Rigidbody.isKinematic = true;

        foreach (var animator in Animators)
        {
            animator.enabled = false;
        }

        foreach (var ragdoll in ragdolls)
        {
            ragdoll.GetComponent<Collider>().isTrigger = false;
            ragdoll.GetComponent<Rigidbody>().isKinematic = false;
            ragdoll.GetComponent<Rigidbody>().AddForce(forceAxis * forceAmount);
        }
    }

    [Button]
    public void EnableRagdoll()
    {
        Ragdoll[] ragdolls = GetComponentsInChildren<Ragdoll>();

        if (Col != null)
            Col.isTrigger = true;

        if (Rigidbody != null)
            Rigidbody.isKinematic = true;

        foreach (var animator in Animators)
        {
            animator.enabled = false;
        }

        foreach (var ragdoll in ragdolls)
        {
            ragdoll.GetComponent<Collider>().isTrigger = false;
            ragdoll.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    [Button]
    public void ResetRagdoll()
    {
        Ragdoll[] ragdolls = GetComponentsInChildren<Ragdoll>();

        if (Col != null)
            Col.isTrigger = false;

        if (Rigidbody != null)
            Rigidbody.isKinematic = false;

        foreach (var animator in Animators)
        {
            animator.enabled = true;
        }

        foreach (var ragdoll in ragdolls)
        {
            ragdoll.GetComponent<Collider>().isTrigger = true;
            ragdoll.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    [Button]
    private void SetupRagdoll()
    {
        Ragdoll[] ragdolls = GetComponentsInChildren<Ragdoll>();

        if (ragdolls.Length == 0)
        {
            Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();

            if (rbs.Length == 0)
            {
                Debug.LogError("There is probably no ragdoll available. Assign a ragdoll before setup.");
                return;
            }
            else
            {
                foreach (var rb in rbs)
                {
                    if (rb.gameObject == gameObject)
                        continue;

                    Ragdoll ragdoll = rb.gameObject.AddComponent<Ragdoll>();
                }
            }
        }

        EnableRagdoll();
        ResetRagdoll();

        ragdolls = GetComponentsInChildren<Ragdoll>();

        foreach (var ragdoll in ragdolls)
        {
            if (ragdoll.gameObject == gameObject)
                continue;

            ragdoll.gameObject.layer = RagdollLayerIndex;
            var characterJoint = ragdoll.GetComponent<CharacterJoint>();
            var rigidbody = ragdoll.GetComponent<Rigidbody>();
            if (characterJoint != null)
            {
                characterJoint.enableProjection = true;
                characterJoint.projectionDistance = 0.01f;
            }

            if (rigidbody != null)
            {
                rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            }
        }

        gameObject.layer = CharacterLayerIndex;
    }

    public Ragdoll RandomRagdoll()
    {
        return Ragdolls[Random.Range(0, Ragdolls.Count)];
    }
}

