using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HCB.SplineMovementSystem
{
    public class Jumper : MonoBehaviour
    {
        [SerializeField]
        private float jumpForce = 500f;
        public float JumpForce { get { return jumpForce; } protected set { jumpForce = value; } }

        private bool _isActive = true;
        private void OnTriggerEnter(Collider other)
        {
            if (!_isActive)
                return;

            IJumpTarget jumpTarget = other.GetComponentInParent<IJumpTarget>();
            if (jumpTarget != null)
            {
                OnTriggered();                             
                jumpTarget.Jump(JumpForce);                
            }
        }
        protected virtual void OnTriggered()
        {
            _isActive = false;
        }
    }
}
