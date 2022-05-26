using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;
using HCB.SplineMovementSystem.Samples;
using Dreamteck.Forever;
using HCB.UI;

public class FinishTrigger : MonoBehaviour
{
    
    private bool isCompleteStage = false;

    private void OnTriggerEnter(Collider other) //trigger'a girdigi zaman digerlerinin componentini alabiliyoruz hangisi girerse.
    {
        if (isCompleteStage)
            return;
        PlayerController player = other.GetComponentInParent<PlayerController>();
        AIMovement AIMovement = other.GetComponentInParent<AIMovement>();
        SplineCharacterAnimationController splineCharacterAnimationController = other.GetComponentInChildren<SplineCharacterAnimationController>();
        

        if (player != null)
        {

            isCompleteStage = true;
            player.Runner.follow = false;
            
            GameManager.Instance.CompeleteStage(true);
            splineCharacterAnimationController.TriggerAnimation("Win");
            HapticManager.Haptic(HapticTypes.Success);

            //Ai bug'i icin ama cozulmuyor :(
            AIMovement.Runner.follow = false;



        }

        else if(AIMovement!= null)
        {
            
            isCompleteStage = true;
            //GameManager.Instance.CompeleteStage(false);
            HapticManager.Haptic(HapticTypes.Failure);
            Events.OnEliminated.Invoke();
          
        }
    }

}
