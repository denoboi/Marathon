using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;
using HCB.SplineMovementSystem.Samples;
using Dreamteck.Forever;

public class FinishTrigger : MonoBehaviour
{


    private void OnTriggerEnter(Collider other) //trigger'a girdigi zaman digerlerinin componentini alabiliyoruz hangisi girerse.
    {
        
        PlayerController player = other.GetComponentInParent<PlayerController>();
        AIMovement AIMovement = other.GetComponentInParent<AIMovement>();
        SplineCharacterAnimationController splineCharacterAnimationController = other.GetComponentInChildren<SplineCharacterAnimationController>();
        

        if (player != null)
        {
            GameManager.Instance.CompeleteStage(true);
            splineCharacterAnimationController.TriggerAnimation("Win");
            HapticManager.Haptic(HapticTypes.Success);
            Debug.Log(gameObject.name);
            
            player.Runner.followSpeed = 0;
        }

        else if(AIMovement!= null)
        {
            GameManager.Instance.CompeleteStage(false);
            HapticManager.Haptic(HapticTypes.Failure);
        }
    }

}
