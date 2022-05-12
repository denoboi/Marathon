using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollision : MonoBehaviour
{
    //if other.tranform.position.z < transform.position.z

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ai"))
        {
            Debug.Log("Hit");
        }
    }
   
}
