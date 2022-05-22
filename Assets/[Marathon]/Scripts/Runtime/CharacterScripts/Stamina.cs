using HCB.Core;
using HCB.SplineMovementSystem;
using HCB.SplineMovementSystem.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float CurrentStamina = 100.0f; 
    
    public float MaxStamina = 100.0f;

    [HideInInspector] public bool IsRegenerated;

    [Header("Stamina Regeneration Parameters")]
    
    [Range(1.25f, 15f)] public float StaminaDrainMultiplier;
    [Range(1.25f, 15f)] public float StaminaRegenMultiplier;

    private SplineCharacter _splineCharacter;
    private SplineCharacterAnimationController _splineCharacterAnimationController;

    public SplineCharacter SplineCharacter { get { return _splineCharacter == null ? _splineCharacter = GetComponent<SplineCharacter>() : _splineCharacter; } }

    public SplineCharacterAnimationController SplineCharacterAnimationController
    { get { return _splineCharacterAnimationController == null ? _splineCharacterAnimationController = GetComponentInChildren<SplineCharacterAnimationController>() : _splineCharacterAnimationController; } }


    public void Update()
    {
        SplineCharacterAnimationController.SetStamina(HCB.Utilities.HCBUtilities.Normalize01(CurrentStamina, MaxStamina));
    }

    public void StaminaDrain()
    {
        CurrentStamina -= Time.deltaTime * StaminaDrainMultiplier;

        if (CurrentStamina <= 0)
        {
            CurrentStamina = 0f;
            Debug.Log("Boom");
            
            //LevelManager.Instance.OnLevelFinish.Invoke();

        }
        
    }

    public void StaminaRegen()
    {
        if (!SplineCharacter.IsControlable)
            return;

        if (!IsRegenerated)
            return;

        if (CurrentStamina < MaxStamina)
        {
            CurrentStamina += Time.deltaTime * StaminaRegenMultiplier;
            if (CurrentStamina > MaxStamina)
                CurrentStamina = MaxStamina;
        }

    }
        
  }
    




