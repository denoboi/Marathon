using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.IncrimantalIdleSystem;
using Dreamteck.Forever;
using HCB.SplineMovementSystem;

public class SpeedUpgrader : IdleStatObjectBase
{
    private Runner _runner;

    public Runner Runner { get { return _runner == null ? _runner = GetComponent<Runner>() : _runner; } }

    private SplineCharacterMovementController _splineCharacter;
    public SplineCharacterMovementController SplineCharacter { get { return _splineCharacter == null ? _splineCharacter = GetComponent<SplineCharacterMovementController>() : _splineCharacter; } }

   

    public override void UpdateStat(string id)
    {
        Runner.followSpeed = (float)IdleStat.CurrentValue;
        SplineCharacter.maxSpeed = (float)IdleStat.CurrentValue;
    }
}
