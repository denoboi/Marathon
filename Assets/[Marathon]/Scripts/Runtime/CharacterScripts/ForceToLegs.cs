using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceToLegs : MonoBehaviour
{
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    private void OnEnable()
    {
        Events.OnPlayerFall.AddListener(AddForce);
    }

    private void OnDisable()
    {
        Events.OnPlayerFall.RemoveListener(AddForce);
    }


    private void OnTriggerEnter(Collider other)
    {
        

        Rigidbody rb = other.GetComponent<Rigidbody>();

        if(rb != null && !rigidbodies.Contains(rb))
        {
            rigidbodies.Add(rb);
        }

       
        
    }

    void AddForce()
    {
        foreach (var rb in rigidbodies)
        {
            rb.AddForce(Vector3.back * 40, ForceMode.Impulse) ;
        }
    }
}
