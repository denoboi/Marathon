using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;

public class Player : SplineCharacter
{
    private int _lastPositionZ;

    private SkinnedMeshRenderer _playerMat;
    private Stamina _stamina;
    private IncomeManager _incomeManager;

    public SkinnedMeshRenderer SkinnedMeshRenderer { get { return _playerMat == null ? _playerMat = GetComponentInChildren<SkinnedMeshRenderer>() : _playerMat; } }
    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }
    public IncomeManager IncomeManager { get { return _incomeManager == null ? _incomeManager = GetComponent<IncomeManager>() : _incomeManager; } }

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
        CheckDistanceTravelled();
    }

    private float NormalizeValue(float value, float min, float max)
    {
        float normalizedValue = (value - min) / (max - min);

        value = Mathf.Clamp(value, min, max);

        normalizedValue = Mathf.Clamp(normalizedValue, 0, 1);

        return normalizedValue;
    }


    private void CheckDistanceTravelled()
    {


        int roundedPos = Mathf.RoundToInt(transform.position.z);

        if (roundedPos > _lastPositionZ)
        {
            _lastPositionZ = roundedPos;


            GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] += (int)IncomeManager.IdleStat.CurrentValue;


            HCB.Core.EventManager.OnPlayerDataChange.Invoke();
            //VisualController.CreateFloatingText("+" + income.statValue.ToString("N1") + " $", Color.green, 1f);
        }




    }
}