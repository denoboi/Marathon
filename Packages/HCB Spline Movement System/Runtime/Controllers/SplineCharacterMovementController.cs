using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dreamteck.Splines;
using HCB.Core;

namespace HCB.SplineMovementSystem
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SplineFollower))]
    public class SplineCharacterMovementController : MonoBehaviour, IJumpTarget
    {
        #region Local Properties
        Rigidbody _rigidbody;
        Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInChildren<Rigidbody>() : _rigidbody;

        SplineCharacter _splineCharacter;
        SplineCharacter SplineCharacter => _splineCharacter == null ? _splineCharacter = GetComponentInChildren<SplineCharacter>() : _splineCharacter;

        SplineFollower _splineFollower;
        SplineFollower SplineFollower => _splineFollower == null ? _splineFollower = GetComponentInChildren<SplineFollower>() : _splineFollower;
        #endregion

        [SerializeField]
        private SplineMovementData movementData;
        public SplineMovementData MovementData { get { return movementData; } set { movementData = value; } }

        public Collider MainCollider;       
        
        private const float BLEND_ROTATION_DURATION = 2f;
        
        private string _rotationTweenId;
        private string _speedTweenId;

        private bool _applyRotationX;
        private bool _applyRotationY;
        private bool _applyRotationZ;

        private float _currentSpeed;
        private void Awake()
        {
            SetupDefaultValues();
            SetupTweenIds();
            SetFollowerInitialPercent();
        }
        private void OnEnable()
        {            
            SplineCharacter.OnSlideStart.AddListener(() => SetSpeed(MovementData.SlideSpeed));
            SplineCharacter.OnSlideStop.AddListener(() => SetSpeed(MovementData.DefaultSpeed));
            SplineCharacter.OnCharacterLocationChanged.AddListener(OnCharacterLocationChanged);            
        }

        private void OnDisable()
        {
            SplineCharacter.OnSlideStart.RemoveListener(() => SetSpeed(MovementData.SlideSpeed));
            SplineCharacter.OnSlideStop.RemoveListener(() => SetSpeed(MovementData.DefaultSpeed));
            SplineCharacter.OnCharacterLocationChanged.RemoveListener(OnCharacterLocationChanged);            
        }
       
        private void Update()
        {
            if (!SplineCharacter.CanMoveForward)
                return;
            
            SplineFollower.Move(_currentSpeed * Time.deltaTime);
        }
        public void Jump(float jumpForce)
        {           
            SplineFollower.motion.applyPositionY = false;
            SplineFollower.motion.applyRotation = false;
            
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.x = 0f;

            DOTween.Kill(_rotationTweenId);
            transform.DORotate(eulerAngles, BLEND_ROTATION_DURATION).SetId(_rotationTweenId);

            MainCollider.isTrigger = false;
            Rigidbody.isKinematic = false;
            Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }        
        private void OnTouchGround()
        {
            Rigidbody.isKinematic = true;
            MainCollider.isTrigger = true;

            SplineFollower.motion.applyPositionY = true;            

            float lerpStep = 0f;
            Quaternion startRotation = transform.rotation;

            DOTween.Kill(_rotationTweenId);
            DOTween.To(() => lerpStep, x => lerpStep = x, 1f, BLEND_ROTATION_DURATION).SetId(_rotationTweenId).OnUpdate(() =>
            {
                transform.rotation = Quaternion.Slerp(startRotation, SplineFollower.result.rotation, lerpStep);
            })
            .OnComplete(() =>
            {                
                SetSplineFollowerApplyRotation(_applyRotationX, _applyRotationY, _applyRotationZ);                             
            });
        }
        private void OnCharacterLocationChanged(CharacterLocationState characterLocationState)
        {
            if (characterLocationState == CharacterLocationState.OnGround)
            {
                OnTouchGround();
            }
        }
        private void SetSpeed(float value)
        {
            DOTween.Kill(_speedTweenId);
            DOTween.To(() => _currentSpeed, x => _currentSpeed = x, value, MovementData.SpeedBlendDuration).SetId(_speedTweenId);
        }      

        private void SetSplineFollowerApplyRotation(bool x, bool y, bool z) 
        {
            SplineFollower.motion.applyRotationX = x;
            SplineFollower.motion.applyRotationY = y;
            SplineFollower.motion.applyRotationZ = z;
        }

        private void SetupTweenIds() 
        {
            _rotationTweenId = gameObject.GetInstanceID() + "RotationTweenId";
            _speedTweenId = gameObject.GetInstanceID() + "SpeedTweenId";
        }

        private void SetupDefaultValues() 
        {
            _currentSpeed = MovementData.DefaultSpeed;

            _applyRotationX = SplineFollower.motion.applyRotationX;
            _applyRotationY = SplineFollower.motion.applyRotationY;
            _applyRotationZ = SplineFollower.motion.applyRotationZ;
        }      

        private void SetFollowerInitialPercent() 
        {            
            SplineFollower.SetPercent(SplineFollower.spline.Project(transform.position).percent);
        }

        private void SetRigidbodyPresets() 
        {
            Rigidbody.isKinematic = true;
            Rigidbody.interpolation = RigidbodyInterpolation.None;
            Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            Rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }

        private void SetSplineFollowerPresets() 
        {
            SplineFollower.follow = false;
            SplineFollower.useTriggers = true;
            SplineFollower.autoStartPosition = true;
        }

        private void Reset()
        {
            SetRigidbodyPresets();
            SetSplineFollowerPresets();
        }

        //We need to rebuild to prevent follow cached spline path. Its Dreamtech's bug.
        private void OnValidate()
        {
            SplineFollower.RebuildImmediate();
        }
    }
}
