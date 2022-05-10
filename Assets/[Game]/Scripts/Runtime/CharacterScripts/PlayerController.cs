using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.SplineMovementSystem;
using HCB.Core;
using HCB.SplineMovementSystem.Samples;

public class PlayerController : SplineCharacterMovementController
{
    SplineCharacterAnimationController _splineCharacterAnimationController;

    SplineCharacterAnimationController SplineCharacterAnimationController 
    { get { return _splineCharacterAnimationController == null ? _splineCharacterAnimationController = GetComponentInChildren<SplineCharacterAnimationController>() : _splineCharacterAnimationController; } }

   

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
        Moving();
        base.Update();
        
    }

    void Moving()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //Animation event invoke
            SplineCharacterAnimationController.TriggerAnimation("Run"); //burada bir bug var hala cozulecek!!!
            
            SplineCharacter.CanMoveForward = true;

        }

        //if mouse button released then stop.
        else if(Input.GetMouseButtonUp(0))
        {
            
            SplineCharacterAnimationController.TriggerAnimation("Idle");
            SplineCharacter.CanMoveForward = false;
        }
    }
}
