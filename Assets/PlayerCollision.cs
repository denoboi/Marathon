using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //this is for AI hitting player
        if (other.CompareTag("Player") && other.transform.position.z > transform.position.z)
        {
            Debug.Log("Hit the player");
        }
    }
}
