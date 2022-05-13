using HCB.SplineMovementSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float CurrentStamina = 100.0f; //isimlendirme sorulacak
    
    private float _maxStamina = 100.0f;
    

    [HideInInspector] public bool IsRegenerated ;


    [Header("Stamina Regeneration Parameters")]
    
    [Range(1.25f, 15f)] public float _staminaDrainMultiplier;
    [Range(1.25f, 15f)] public float _staminaRegenMultiplier;

    private SplineCharacter _splineCharacter;

    public SplineCharacter SplineCharacter { get { return _splineCharacter == null ? _splineCharacter = GetComponent<SplineCharacter>() : _splineCharacter; } }



    public void StaminaDrain()
    {
        if (IsRegenerated)
            return;

        CurrentStamina -= Time.deltaTime * _staminaDrainMultiplier;

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

        if (CurrentStamina < _maxStamina)
        {
            CurrentStamina += Time.deltaTime * _staminaRegenMultiplier;
            if (CurrentStamina > _maxStamina)
                CurrentStamina = _maxStamina;
        }

       
        
    }
        
  }
    




