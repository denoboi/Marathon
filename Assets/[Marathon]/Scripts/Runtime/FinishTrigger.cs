using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;
using HCB.SplineMovementSystem.Samples;
using Dreamteck.Forever;

public class FinishTrigger : MonoBehaviour
{
    private SplineCharacterAnimationController _splineCharacterAnimationController;
    private SplineCharacter splineCharacter;
    private Runner _runner;

    public Runner Runner { get { return _runner == null ? _runner = GetComponentInParent<Runner>() : _runner; } }

    public SplineCharacterAnimationController SplineCharacterAnimationController
    { get { return _splineCharacterAnimationController == null ? _splineCharacterAnimationController = GetComponentInChildren<SplineCharacterAnimationController>() : _splineCharacterAnimationController; } }





    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            GameManager.Instance.OnStageSuccess.Invoke();

        }
        else
            GameManager.Instance.OnStageFail.Invoke();
    }

    

}
