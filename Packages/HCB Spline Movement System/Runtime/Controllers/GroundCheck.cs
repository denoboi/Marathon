using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.SplineMovementSystem
{
    public class GroundCheck : MonoBehaviour
    {
        SplineCharacter _splineCharacter;
        SplineCharacter SplineCharacter => _splineCharacter == null ? _splineCharacter = GetComponentInParent<SplineCharacter>() : _splineCharacter;
                
        public LayerMask GroundLayer;
        public LayerMask PlatformLayer;


        private const float RAY_OFFSET = 0.1f;
        
        private float _rayDistance;
        private void Awake()
        {            
            _rayDistance = transform.position.y + RAY_OFFSET;
        }
        private void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, Vector3.down, _rayDistance, GroundLayer))
            {
                SplineCharacter.TryChangeLocationState(CharacterLocationState.OnGround);
            }

            else if (Physics.Raycast(transform.position, Vector3.down, _rayDistance, PlatformLayer))
            {
                SplineCharacter.TryChangeLocationState(CharacterLocationState.OnPlatform);
            }

            else
            {
                SplineCharacter.TryChangeLocationState(CharacterLocationState.OnAir);
            }
        }       
    }
}
