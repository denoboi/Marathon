using HCB.SplineMovementSystem;
using HCB.SplineMovementSystem.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollision : MonoBehaviour
{
    //if other.tranform.position.z < transform.position.z

    private SplineCharacterAnimationController _splineCharacterAnimationController;
    private SplineCharacterMovementController _splineCharacterMovementController;
    private PlayerController _playerController;
    private SplineCharacter splineCharacter;

    public SplineCharacterAnimationController SplineCharacterAnimationController
    { get { return _splineCharacterAnimationController == null ? _splineCharacterAnimationController = GetComponentInChildren<SplineCharacterAnimationController>() : _splineCharacterAnimationController; } }

    public SplineCharacterMovementController SplineCharacterMovementController
    { get { return _splineCharacterMovementController == null ? _splineCharacterMovementController = GetComponentInParent<SplineCharacterMovementController>() : _splineCharacterMovementController; } }

    public PlayerController PlayerController { get { return _playerController == null ? _playerController = GetComponentInParent<PlayerController>() : _playerController; } }

    //spline character yok? Olmamasina ragmen kalitim aldigimiz icin mi goruyor? 
    public SplineCharacter SplineCharacter { get { return splineCharacter == null ? splineCharacter = GetComponentInParent<SplineCharacter>() : splineCharacter; } }

    private void OnTriggerEnter(Collider other)
    {

        
        if(other.CompareTag("Character"))
        {
            if (transform.position.z < other.transform.position.z)
            {
                Debug.Log(gameObject.name + "Collided");
                
                StartCoroutine(WaitForMoveForward());
                
                
            }
                
                
        }
    }

    IEnumerator WaitForMoveForward()
    {
        SplineCharacterAnimationController.TriggerAnimation("Fall");
        SplineCharacter.CanMoveForward = false;
        SplineCharacter.IsControlable = false;

        yield return new WaitForSeconds(3);

        SplineCharacter.CanMoveForward = true;  //burada kucuk bir bug var eger player duserse bekledikten sonra biz tiklamadan kosmaya devam ediyor.
        SplineCharacterAnimationController.TriggerAnimation("Run");
        SplineCharacter.IsControlable = true;

    }


   
}
