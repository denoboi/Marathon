using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HCB.Core;

namespace HCB.SplineMovementSystem
{
    public enum CharacterLocationState {None, OnAir, OnGround, OnPlatform}
    public class LocationEvent : UnityEvent<CharacterLocationState> { }

    [RequireComponent(typeof(SplineCharacterMovementController))]
    public class SplineCharacter : MonoBehaviour 
    {
        #region Properties
        private bool _canMoveForward;
        public bool CanMoveForward { get { return _canMoveForward; } set { _canMoveForward = value; } }

        private bool _isControlable;
        public bool IsControlable { get { return _isControlable; } set { _isControlable = value; } }

        private bool _isInSlideArea;
        public bool IsInSlideArea { get { return _isInSlideArea;} private set { _isInSlideArea = value; } }

        private bool _isSliding;
        public bool IsSliding { get { return _isSliding; } private set { _isSliding = value; } }

        //this is mine
        private bool _isFinished;

        public bool IsFinished { get { return _isFinished; } protected set { _isFinished = value; } }

        private CharacterLocationState _previousCharacterLocationState = CharacterLocationState.OnGround;
        public CharacterLocationState PreviousCharacterLocationState { get { return _previousCharacterLocationState; } private set { _previousCharacterLocationState = value; } }

        private CharacterLocationState _characterLocationState = CharacterLocationState.OnGround;
        public CharacterLocationState CurrentCharacterLocationState { get { return _characterLocationState; } private set { _characterLocationState = value; } }
        #endregion

        #region Events
        [HideInInspector]
        public LocationEvent OnCharacterLocationChanged = new LocationEvent();
        [HideInInspector]
        public UnityEvent OnSlideStart = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnSlideStop = new UnityEvent();
        #endregion

        protected virtual void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            // LevelManager.Instance.OnLevelStart.AddListener(OnLevelStart);
            EventManager.OnCountDownEnd.AddListener(OnLevelStart);
            GameManager.Instance.OnStageSuccess.AddListener(OnLevelEnd);
            GameManager.Instance.OnStageFail.AddListener(OnLevelEnd);            
        }

        protected virtual void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            //LevelManager.Instance.OnLevelStart.RemoveListener(OnLevelStart);
            EventManager.OnCountDownEnd.AddListener(OnLevelStart); 
            GameManager.Instance.OnStageSuccess.RemoveListener(OnLevelEnd);
            GameManager.Instance.OnStageFail.RemoveListener(OnLevelEnd);
        }

        public void TryChangeLocationState(CharacterLocationState characterLocationState)
        {
            if (CurrentCharacterLocationState == characterLocationState)
                return;

            PreviousCharacterLocationState = CurrentCharacterLocationState;
            CurrentCharacterLocationState = characterLocationState;

            OnCharacterLocationChanged.Invoke(CurrentCharacterLocationState);
            
            CheckSlideStatus();
        }

        public void OnEnterSlideArea() 
        {
            IsInSlideArea = true;
            TryStartSlide();
        }

        public void OnExitSlideArea() 
        {
            IsInSlideArea = false;
            StopSlide();
        }

        public virtual void OnFinishTriggered() 
        {
            GameManager.Instance.CompeleteStage(true);

            //for finish check
            IsFinished = true;
        } 

        private void CheckSlideStatus() 
        {   
            if (CurrentCharacterLocationState != CharacterLocationState.OnGround)
            {
                StopSlide();
            }

            else 
            {
                TryStartSlide();
            }
        }

        private void TryStartSlide()
        {
            if (!IsSliding && IsInSlideArea && CurrentCharacterLocationState == CharacterLocationState.OnGround)
            {
                IsSliding = true;
                OnSlideStart.Invoke();                
            }           
        }   

        private void StopSlide()
        {
            if (IsSliding)
            {
                IsSliding = false;
                OnSlideStop.Invoke();
            }
        }

        private void OnLevelStart()
        {

            IsControlable = true;
            CanMoveForward = true;
        }

        private void OnLevelEnd()
        {
            IsControlable = false;
            CanMoveForward = false;
        }
    }
}
