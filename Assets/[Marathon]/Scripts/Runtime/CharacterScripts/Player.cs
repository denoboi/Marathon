using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;
using TMPro;
using HCB.PoolingSystem;
using DG.Tweening;
using UnityEngine.UI;

public class Player : SplineCharacter
{
    private int _lastPositionZ;

    private SkinnedMeshRenderer _playerMat;
    private Stamina _stamina;
    private IncomeManager _incomeManager;
    private TextMeshProUGUI _textMeshProUGUI;
    private SplineCharacterClampController _splineCharacterClampController;

    public SkinnedMeshRenderer SkinnedMeshRenderer { get { return _playerMat == null ? _playerMat = GetComponentInChildren<SkinnedMeshRenderer>() : _playerMat; } }
    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }
    public IncomeManager IncomeManager { get { return _incomeManager == null ? _incomeManager = GetComponent<IncomeManager>() : _incomeManager; } }
    public SplineCharacterClampController SplineCharacterClampController { get { return _splineCharacterClampController == null ? _splineCharacterClampController = GetComponentInChildren<SplineCharacterClampController>() : _splineCharacterClampController; } }

    private float _normalizeStamina;
    private float maximumStamina;

    [SerializeField] private ParticleSystem _sweatingParticle;
    [SerializeField] private float _headChangeSpeed;

    private void Awake()
    {
        maximumStamina = Stamina.MaxStamina / 2;

    }


    void TiredMaterial()
    {
        
        _normalizeStamina = NormalizeValue(Stamina.CurrentStamina, 0, maximumStamina); // bunu tam anlamadim

        SkinnedMeshRenderer.material.SetFloat("_Postion", _normalizeStamina);

        if(Stamina.CurrentStamina < maximumStamina / 2)
        {
            Sweat();
            SkinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.Clamp(Mathf.Sin(Time.time * _headChangeSpeed) * 100, 0, 100));
        }
        else
        {
            StopSweat();
            SkinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.Lerp(SkinnedMeshRenderer.GetBlendShapeWeight(0), 0, Time.deltaTime * _headChangeSpeed));
        }
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
            CreateFloatingText("+" + IncomeManager.IdleStat.CurrentValue.ToString("N0") + " $", Color.green, 1f);
        }

    }
        public void CreateFloatingText(string s, Color color, float delay)
        {
            TextMeshPro text = PoolingSystem.Instance.InstantiateAPS("text", SplineCharacterClampController.gameObject.transform.position).GetComponent<TextMeshPro>();
            text.transform.LookAt(Camera.main.transform);
            text.SetText(s);
            text.DOFade(1, 0);
            text.color = color;
            text.transform.DOMoveY(text.transform.position.y + 1f, delay);
            text.DOFade(0, delay / 2)
                .SetDelay(delay / 2)
                .OnComplete(() => PoolingSystem.Instance.DestroyAPS(text.gameObject));
        }

    private void Sweat()
    {
        var emission = _sweatingParticle.emission;
        emission.rateOverTime = 30;
    }

    public void StopSweat()
    {
        var emission = _sweatingParticle.emission;
        emission.rateOverTime = 0;
    }

}