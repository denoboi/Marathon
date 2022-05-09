using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.SplineMovementSystem
{
    public class SplineMovementData : ScriptableObject
    {
        public float DefaultSpeed = 6;
        public float SlideSpeed = 8;
        [Tooltip("The blend duration that uses when character speed changed.")]
        public float SpeedBlendDuration = 1;
    }
}
