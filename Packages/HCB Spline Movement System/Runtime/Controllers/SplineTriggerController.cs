using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

namespace HCB.SplineMovementSystem
{
    public class SplineTriggerController : MonoBehaviour
    {       
        public void OnEnterSlideArea(SplineUser splineUser) 
        {            
            SplineCharacter splineCharacter = splineUser.GetComponentInParent<SplineCharacter>();
            if (splineCharacter == null)
                return;

            splineCharacter.OnEnterSlideArea();            
        }

        public void OnExitSlideArea(SplineUser splineUser) 
        {           
            SplineCharacter splineCharacter = splineUser.GetComponentInParent<SplineCharacter>();
            if (splineCharacter == null)
                return;

            splineCharacter.OnExitSlideArea();            
        }

        public void OnFinishTriggered(SplineUser splineUser)
        {
            SplineCharacter splineCharacter = splineUser.GetComponentInParent<SplineCharacter>();
            if (splineCharacter == null)
                return;

            splineCharacter.OnFinishTriggered();
        }        
    }
}
