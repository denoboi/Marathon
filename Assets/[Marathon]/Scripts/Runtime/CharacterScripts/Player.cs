using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;

public class Player : MonoBehaviour
{
    CapsuleCollider _capsuleCollider;

    CapsuleCollider CapsuleCollider { get { return _capsuleCollider == null ? GetComponentInChildren<CapsuleCollider>() : _capsuleCollider; } }

    //hepsinin kullandigi bir componenti get et.

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("AI"))
        {
            Debug.Log("Collided");
        }
    }

}
