using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using DG.Tweening;

namespace HCB.SplineMovementSystem.Samples
{
    public class SplineCharacterAnimationController : MonoBehaviour
    {
        Animator _animator;

        //buna playercontroller'da ulasmak icin public yaptik asagida da metod olusturduk.
        public Animator Animator => _animator == null ? _animator = GetComponentInParent<Animator>() : _animator;



        SplineCharacter _splineCharacter;
        SplineCharacter SplineCharacter => _splineCharacter == null ? _splineCharacter = GetComponentInParent<SplineCharacter>() : _splineCharacter;

        SplineCharacterMovementController _splineCharacterMovementController;
        SplineCharacterMovementController SplineCharacterMovementController => _splineCharacterMovementController == null ? _splineCharacterMovementController = GetComponentInParent<SplineCharacterMovementController>() : _splineCharacterMovementController;


        private const string SPEED_PARAMETER = "Speed";
        private const string STAMINA_PARAMETER = "Stamina";
        private const string JUMP_PARAMETER = "Jump";
        private const string ROLL_PARAMETER = "Roll";
        private const string RUN_PARAMETER = "Run";
        private const string SLIDE_PARAMETER = "IsSliding";
        private const string VICTORY_PARAMETER = "Victory";

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            LevelManager.Instance.OnLevelStart.AddListener(() => Animator.SetTrigger(RUN_PARAMETER));
            GameManager.Instance.OnStageSuccess.AddListener(OnSucces);
            SplineCharacter.OnCharacterLocationChanged.AddListener(OnCharacterLocationChanged);
            SplineCharacter.OnSlideStart.AddListener(OnSlideStart);
            SplineCharacter.OnSlideStop.AddListener(OnSlideStop);
            EventManager.OnCountDownReady.AddListener(Crouch);
            EventManager.OnCountDownSet.AddListener(Crouch2);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            LevelManager.Instance.OnLevelStart.RemoveListener(() => Animator.SetTrigger(RUN_PARAMETER));
            GameManager.Instance.OnStageSuccess.RemoveListener(OnSucces);
            SplineCharacter.OnCharacterLocationChanged.RemoveListener(OnCharacterLocationChanged);
            SplineCharacter.OnSlideStart.RemoveListener(OnSlideStart);
            SplineCharacter.OnSlideStop.RemoveListener(OnSlideStop);
            EventManager.OnCountDownReady.RemoveListener(Crouch);
            EventManager.OnCountDownSet.RemoveListener(Crouch2);
        }

        private void Update()
        {
            Animator.SetFloat(SPEED_PARAMETER, SplineCharacterMovementController.CurrentSpeed());

        }

        public void SetStamina(float value)
        {
            Animator.SetFloat(STAMINA_PARAMETER, value);
        }

        private void OnCharacterLocationChanged(CharacterLocationState characterLocationState) 
        {
            switch (characterLocationState)
            {
                case CharacterLocationState.OnAir:
                    Animator.SetTrigger(JUMP_PARAMETER);
                    break;

                case CharacterLocationState.OnGround:
                    if (SplineCharacter.PreviousCharacterLocationState == CharacterLocationState.OnAir)
                        Animator.SetTrigger(ROLL_PARAMETER);
                    break;

                case CharacterLocationState.OnPlatform:
                    Animator.SetTrigger(RUN_PARAMETER);
                    break;

                default:
                    break;
            }
        }

        private void OnSlideStart() 
        {
            Animator.SetBool(SLIDE_PARAMETER, true);
        }

        private void OnSlideStop() 
        {
            Animator.SetBool(SLIDE_PARAMETER, false);            
        }

        private void OnSucces() 
        {
            Animator.SetTrigger(VICTORY_PARAMETER);

            //Return character for Victory Animation
            float duration = 0.5f;
            Animator.transform.DOKill();
            Animator.transform.DOLocalRotate(new Vector3(0f, 180f, 0f), duration);
        }


        //bu metodu playercontroller'da animasyonlari invokelamak icin actik
        public void TriggerAnimation(string ID)
        {
            Animator.SetTrigger(ID);
        }

        public void BoolAnimation(string ID, bool value)
        {
            Animator.SetBool(ID, value);
        }

        private void Crouch()
        {
            Animator.SetTrigger("Crouch");
        }

        private void Crouch2()
        {
            Animator.SetTrigger("Crouch2");
        }

    }
}
