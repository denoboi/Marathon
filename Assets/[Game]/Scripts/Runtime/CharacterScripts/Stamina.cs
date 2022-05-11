using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float PlayerStamina = 100.0f;
    public float AIStamina = 100.0f;
    public float _maxStamina = 100.0f;
    

    [HideInInspector] public bool IsRegenerated;


    [Header("Stamina Regeneration Parameters")]
    
    [Range(1.25f, 15f)] public float _staminaDrainMultiplier;
    [Range(1.25f, 15f)] public float _staminaRegenMultiplier;



    void StaminaDrain()
    {
        PlayerStamina -= Time.deltaTime * Stamina._staminaDrainMultiplier;
    }
    



}
