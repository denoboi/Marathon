using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float PlayerStamina = 100.0f;
    public float _maxStamina = 100.0f;
    public float OpponentStamina = 100.0f;

    [HideInInspector] public bool IsRegenerated = true;


    [Header("Stamina Regeneration Parameters")]
    
    [Range(1f, 15f)] public float _staminaDrainMultiplier;
    [Range(1f, 15f)] public float _staminaRegenMultiplier;




}
