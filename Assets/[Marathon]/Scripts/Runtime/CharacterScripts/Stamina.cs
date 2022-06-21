using HCB.Core;
using HCB.SplineMovementSystem;
using HCB.SplineMovementSystem.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stamina : MonoBehaviour
{

    [Header("Stamina Main Parameters")]

    [SerializeField] private float _currentStamina = 100f;

        public float MaxStamina = 100.0f;

    public float CurrentStamina
    {
        get
        {
            return _currentStamina;
        }

        set
        {
            _currentStamina = value < 0 ? 0 : value;
        }
    }

    [HideInInspector] public bool IsRegenerated;

    [Header("Stamina Regeneration Parameters")]
    
    [Range(1.25f, 15f)] public float StaminaDrainMultiplier;
    [Range(1.25f, 15f)] public float StaminaRegenMultiplier;

    private SplineCharacter _splineCharacter;
    private SplineCharacterAnimationController _splineCharacterAnimationController;

    
    private string _staminaTweenID;
    private const float STAMINATWEENDURATION = 0.35F;


    public SplineCharacter SplineCharacter { get { return _splineCharacter == null ? _splineCharacter = GetComponent<SplineCharacter>() : _splineCharacter; } }

    public SplineCharacterAnimationController SplineCharacterAnimationController
    { get { return _splineCharacterAnimationController == null ? _splineCharacterAnimationController = GetComponentInChildren<SplineCharacterAnimationController>() : _splineCharacterAnimationController; } }


    private void Awake()
    {
        _staminaTweenID = GetInstanceID() + "_staminaTweenID";
    }

    public void StaminaTween(float endValue)
    {
        DOTween.Kill(_staminaTweenID);
        DOTween.To(() => CurrentStamina, x => CurrentStamina = x, endValue, STAMINATWEENDURATION).SetId(_staminaTweenID).SetEase(Ease.Linear);
    }

    public void Update()
    {
        //SplineCharacterAnimationController.SetStamina(HCB.Utilities.HCBUtilities.Normalize01(CurrentStamina, MaxStamina));
    }

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
        
       

        if (CurrentStamina < MaxStamina)
        {
            CurrentStamina += Time.deltaTime * StaminaRegenMultiplier;
            if (CurrentStamina > MaxStamina)
                CurrentStamina = MaxStamina;
        }

    }
        
  }
    




