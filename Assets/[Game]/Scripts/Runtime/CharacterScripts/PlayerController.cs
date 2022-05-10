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
        StaminaRegen();
        Moving();
        base.Update();
        
    }

    void Moving()
    {

        if (Input.GetMouseButton(0))
        {
            //Animation event invoke
            SplineCharacterAnimationController.TriggerAnimation("Run"); //burada bir bug var cozulecek!!! Cok tikladigimda animasyona giriyor.

            //staminaDrain
            Stamina.PlayerStamina -= Time.deltaTime * Stamina._staminaDrainMultiplier;
            if (Stamina.PlayerStamina <= 0)
            {
                Stamina.PlayerStamina = 0f;
                Debug.Log("Boom");

            }
                

            SplineCharacter.CanMoveForward = true;

            //this is for update check. 
            Stamina.IsRegenerated = false;

        }

        
        if(Input.GetMouseButtonUp(0))
        {
            
            //Stamina can regenerate again.
            Stamina.IsRegenerated = true;

            SplineCharacterAnimationController.TriggerAnimation("Idle");

            //if mouse button released then stop.
            SplineCharacter.CanMoveForward = false;
        }
    }

    void StaminaRegen()
    {

        if (!Player.IsControlable)
            return;

        if (!Stamina.IsRegenerated)
            return;

        if (Stamina.PlayerStamina < Stamina._maxStamina)
        {
            Stamina.PlayerStamina += Time.deltaTime * Stamina._staminaRegenMultiplier;
            if (Stamina.PlayerStamina > Stamina._maxStamina)
                Stamina.PlayerStamina = Stamina._maxStamina;
        }
    }

    
}
