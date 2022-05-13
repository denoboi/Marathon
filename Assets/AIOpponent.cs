using HCB.Core;
using HCB.SplineMovementSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOpponent : SplineCharacter
{
    public override void OnFinishTriggered()
    {
        GameManager.Instance.CompeleteStage(false);

        IsFinished = true ;
    }

}
