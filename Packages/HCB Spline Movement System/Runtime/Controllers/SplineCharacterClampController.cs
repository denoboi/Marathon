using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.SplineMovementSystem
{
    public class SplineCharacterClampController : ClampLeanDragTranslate
    {
        SplineCharacter _splineCharacter;
        SplineCharacter SplineCharacter => _splineCharacter == null ? _splineCharacter = GetComponentInParent<SplineCharacter>() : _splineCharacter;
              
        private Vector3 _targetRotation = Vector3.zero;
        protected override void Update()
        {
            if (!SplineCharacter.IsControlable)
                return;

            base.Update();
            Rotate();
        }

        protected override void Delta(Vector3 screenDelta)
        {
            base.Delta(screenDelta);

            if (EnableBodyRotation) 
            {
                _targetRotation.y += screenDelta.x;
                _targetRotation.y = Mathf.Clamp(_targetRotation.y, MinRotateAngle, MaxRotateAngle);
            }     
        }

        private void Rotate() 
        {
            if (!EnableBodyRotation)
                return;

            RotateBody.localRotation = Quaternion.Slerp(RotateBody.localRotation, Quaternion.Euler(_targetRotation), Time.deltaTime * RotateSpeed);
            _targetRotation.y = Mathf.Lerp(_targetRotation.y, 0f, Time.deltaTime * RecoverySpeed);
        }
#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();
            Use.IgnoreStartedOverGui = false;
        }
#endif
    }
}
