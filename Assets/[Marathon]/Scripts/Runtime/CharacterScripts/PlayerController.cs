using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.SplineMovementSystem;
using HCB.Core;
using HCB.SplineMovementSystem.Samples;
                    
public class PlayerController : SplineCharacterMovementController //default olarak splinemovement sabit hizda
{
    private SplineCharacterAnimationController _splineCharacterAnimationController;
    private Stamina _stamina;
    private Player _player;

    public SplineCharacterAnimationController SplineCharacterAnimationController 
    { get { return _splineCharacterAnimationController == null ? _splineCharacterAnimationController = GetComponentInChildren<SplineCharacterAnimationController>() : _splineCharacterAnimationController; } }

    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>(): _stamina; } }

    public Player Player { get { return _player == null ? _player = GetComponent<Player>() : _player; } }


    //these are for acceleration / deceleration
   
   [SerializeField] private float _maxSpeed = 10f;
    
    public float AccelerationTime = 60;
   [SerializeField] private float _minSpeed;
   [SerializeField] private float _time;


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
        //Animation event invoke
        if (Input.GetMouseButtonDown(0))
        {
            SplineCharacterAnimationController.TriggerAnimation("Run"); //bug cozuldu nasil cozuldu hatirlamiyorum :D
        }
            

        if (Input.GetMouseButton(0))
        {
            //staminaDrain
            Stamina.StaminaDrain();

            SplineCharacter.CanMoveForward = true;

            //this is for the update check. 
            Stamina.IsRegenerated = false;

            if (SplineCharacter.IsSliding)
            {
                Stamina.IsRegenerated = true;
                _currentSpeed = 15;
            }

            else
            {
                _currentSpeed = 6;
            }

        }

        
        if(Input.GetMouseButtonUp(0))
        {

            Stamina.IsRegenerated = true;

            SplineCharacterAnimationController.TriggerAnimation("Idle");

            //if mouse button released then stop.
            SplineCharacter.CanMoveForward = false;

            if (Stamina.CurrentStamina <= 50 )
            {
                SplineCharacterAnimationController.TriggerAnimation("Tired");
            }

            else
            TiredToIdle();

        }

        
      
        
    }

    //this is for when not moving and not tired
    void TiredToIdle()
    {
        if (SplineCharacter.CanMoveForward)
            return;

        if(Stamina.CurrentStamina >= 60)
        {
            SplineCharacterAnimationController.BoolAnimation("IsRefreshed", true);
        }
    }

    #endregion

  

    
}
