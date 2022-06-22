using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

public class ConveyorBelt : MonoBehaviour
{
    private Rigidbody rigidbody;
    public Rigidbody Rigidbody { get { return (rigidbody == null) ? rigidbody = GetComponent<Rigidbody>() : rigidbody; } }



    private float ConveyorSpeed = 50000;


    private void FixedUpdate()
    {

        Vector3 pos = Rigidbody.position;
        Rigidbody.position += Vector3.back * ConveyorSpeed * Time.fixedDeltaTime;
        Rigidbody.MovePosition(pos);
    }
}
