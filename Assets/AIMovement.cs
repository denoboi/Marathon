using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.SplineMovementSystem;

public class AIMovement : SplineCharacterMovementController
{
    
    private Stamina _stamina;

    protected override void Awake()
    {
        base.Awake();
        AIMove();
    }

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
        
        base.Update();

    }

    void AIMove()
    {
        //Speed'i degistirmek icin currentSpeed'i kullan scriptable objelere dokunma.
        _currentSpeed = Random.Range(2, 7);

    }
}
