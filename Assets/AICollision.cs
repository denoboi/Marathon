using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollision : MonoBehaviour
{
    //if other.tranform.position.z < transform.position.z

    

    private void OnTriggerEnter(Collider other)
    {
        //this is for player hitting Ai
        if(other.CompareTag("Ai") && other.transform.position.z > transform.position.z)
        {
            Debug.Log("Hit the AI");
        }
    }


   
}
