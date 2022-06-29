using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.SplineMovementSystem;
using HCB.Core;
using HCB.SplineMovementSystem.Samples;
using Dreamteck.Forever;
using HCB.Utilities;
                    
public class PlayerController : SplineCharacterMovementController //default olarak splinemovement sabit hizda
{
    private SplineCharacterAnimationController _splineCharacterAnimationController;
    private Stamina _stamina;
    private Player _player;
    private Runner _runner;
    public bool CountDownMove;
    private bool _isFail;

    public Runner Runner { get { return _runner == null ? _runner = GetComponent<Runner>() : _runner; } }

    public SplineCharacterAnimationController SplineCharacterAnimationController 
    { get { return _splineCharacterAnimationController == null ? _splineCharacterAnimationController = GetComponentInChildren<SplineCharacterAnimationController>() : _splineCharacterAnimationController; } }

    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>(): _stamina; } }

    public Player Player { get { return _player == null ? _player = GetComponent<Player>() : _player; } }

  

    protected override void OnEnable()
    {
        //splineCharacterMovementController'daki base bu, once yazarsak bunu aliyor.
        base.OnEnable();
        

    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Update()
    {
        Stamina.StaminaRegen();
        Moving();
        base.Update();
       
    }

    #region Movement

    void Moving()
    {
        if (GameManager.Instance.IsStageCompleted)
            return;
        if (!SplineCharacter.IsControlable)
            return;
       

        //Animation event invoke
        if (Input.GetMouseButtonDown(0))
        {
            CheckFail();

            if (Stamina.CurrentStamina <= 50)
                return;

            
        }
            

        if (Input.GetMouseButton(0))
        {
            //staminaDrain
            Stamina.StaminaDrain();
            

            SplineCharacter.CanMoveForward = true;

            //this is for the update check. 
            Stamina.IsRegenerated = false;

            //Slide
            if (SplineCharacter.IsSliding)
            {
                Stamina.IsRegenerated = true;
                _currentSpeed = 15;
            }

            else
            {
                _currentSpeed = 6;
            }

            //Death
            if (Stamina.CurrentStamina <= 0)
            {
                SplineCharacterAnimationController.TriggerAnimation("Dead");
                HapticManager.Haptic(HapticTypes.Failure);
                SplineCharacter.IsControlable = false;
                SplineCharacter.CanMoveForward = false;
                StartCoroutine(Dead());
                Player.GetComponentInChildren<CapsuleCollider>().enabled = false;
            }

        }

        IEnumerator Dead()
        {
            yield return new WaitForSeconds(3.5f);
            GameManager.Instance.CompeleteStage(false);
        }

        
        if(Input.GetMouseButtonUp(0))
        {
            if (SplineCharacter.IsFinished)
                return;

            Stamina.IsRegenerated = true;

            //if mouse button released then stop.
            SplineCharacter.CanMoveForward = false;
        }

        //boyle bi sey yapilabiliyomus :D
        Runner.follow = SplineCharacter.CanMoveForward;
    }



   

    void CheckFail()
    {

        SplineCharacterAnimationController.TriggerAnimation("Crouch");

        if (_isFail)
            return;

        if (GameManager.Instance.IsCountdown)
        {
            _isFail = true;
            SplineCharacterAnimationController.TriggerAnimation("Fail");
            Run.After(2f, () => { GameManager.Instance.CompeleteStage(false); });  
        }
    }
}

    #endregion

  


