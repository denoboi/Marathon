using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.SplineMovementSystem
{
    public class SplineClampData : ScriptableObject
    {
        public float MinRotateAngle = -30f;
        public float MaxRotateAngle = 30f;        
        public float MovementWidth = 4f;
        [Tooltip("The character rotation speed that when we move horizontal")]
        public float RotateSpeed = 10f;
        [Tooltip("The character rotation speed that when we stop moving horizontal and the character will try recover its orjinal rotation with this speed.")]
        public float RecoverySpeed = 15f;
        [Tooltip("Higher value allow faster horizontal movement.")]
        public float Sensitivity = 2.5f;
    }
}
