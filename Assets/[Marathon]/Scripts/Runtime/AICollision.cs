using HCB.SplineMovementSystem;
using HCB.SplineMovementSystem.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Forever;

public class AICollision : MonoBehaviour
{
    //if other.tranform.position.z < transform.position.z

    private SplineCharacterAnimationController _splineCharacterAnimationController;
    private SplineCharacter splineCharacter;
    private Runner _runner;

    public Runner Runner { get { return _runner == null ? _runner = GetComponentInParent<Runner>() : _runner; } }

    public SplineCharacterAnimationController SplineCharacterAnimationController
    { get { return _splineCharacterAnimationController == null ? _splineCharacterAnimationController = GetComponentInChildren<SplineCharacterAnimationController>() : _splineCharacterAnimationController; } }

    

    //spline character yok? Olmamasina ragmen kalitim aldigimiz icin mi goruyor? 
    public SplineCharacter SplineCharacter { get { return splineCharacter == null ? splineCharacter = GetComponentInParent<SplineCharacter>() : splineCharacter; } }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Character"))
        {
            if (transform.position.z < other.transform.position.z)
            {
                Debug.Log(gameObject.name + "Collided");
                
                StartCoroutine(WaitForMoveForward());
            }

            SplineCharacterAnimationController.TriggerAnimation("Stumble");

            Runner.follow = SplineCharacter.CanMoveForward;
            Runner.follow = SplineCharacter.IsControlable;
        }
    }

    IEnumerator WaitForMoveForward()
    {
        SplineCharacterAnimationController.TriggerAnimation("Fall");
        SplineCharacter.CanMoveForward = false;
        SplineCharacter.IsControlable = false;

        yield return new WaitForSeconds(3);

        if(Input.GetMouseButton(0))
        {
            SplineCharacter.CanMoveForward = true;  //burada kucuk bir bug var onemli olmayabilir. eger player duserse bekledikten sonra biz tiklamadan kosmaya devam ediyor. (Aslinda onemli tiklamadigim icin stamina dusmuyor)
            SplineCharacterAnimationController.TriggerAnimation("Run");
            SplineCharacter.IsControlable = true;
        }
       

        

    }

    


   
}
