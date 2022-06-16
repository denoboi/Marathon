using HCB.Core;
using HCB.SplineMovementSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOpponent : SplineCharacter
{
    private SkinnedMeshRenderer _playerMat;
    private Stamina _stamina;

    public SkinnedMeshRenderer SkinnedMeshRenderer { get { return _playerMat == null ? _playerMat = GetComponentInChildren<SkinnedMeshRenderer>() : _playerMat; } }
    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }

    private float _normalizeStamina;
    private float maximumStamina;


    private void Awake()
    {
        maximumStamina = Stamina.MaxStamina / 2;
    }

    void TiredMaterial()
    {
        _normalizeStamina = NormalizeValue(Stamina.CurrentStamina, 0, maximumStamina);

        SkinnedMeshRenderer.material.SetFloat("_Postion", _normalizeStamina);
    }

    private void Update()
    {
        TiredMaterial();
    }

    private float NormalizeValue(float value, float min, float max)
    {
        float normalizedValue = (value - min) / (max - min);

        value = Mathf.Clamp(value, min, max);

        normalizedValue = Mathf.Clamp(normalizedValue, 0, 1);

        return normalizedValue;
    }


}
