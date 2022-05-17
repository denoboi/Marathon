using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineAdding : MonoBehaviour
{
    SplineComputer splineComputer;

    public SplineComputer SplineComputer { get { return splineComputer == null ? splineComputer = GetComponent<SplineComputer>() : splineComputer; } }

    void SplineAdd()
    { }
    

}
