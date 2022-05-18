using HCB.SplineMovementSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float CurrentStamina = 100.0f; //isimlendirme sorulacak
    
    public float MaxStamina = 100.0f;

    [HideInInspector] public bool IsRegenerated;

    [Header("Stamina Regeneration Parameters")]
    
    [Range(1.25f, 15f)] public float StaminaDrainMultiplier;
    [Range(1.25f, 15f)] public float StaminaRegenMultiplier;

    private SplineCharacter _splineCharacter;

    public SplineCharacter SplineCharacter { get { return _splineCharacter == null ? _splineCharacter = GetComponent<SplineCharacter>() : _splineCharacter; } }

    public void StaminaDrain()
    {
        CurrentStamina -= Time.deltaTime * StaminaDrainMultiplier;

        if (CurrentStamina <= 0)
        {
            CurrentStamina = 0f;
            Debug.Log("Boom");
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
    




