using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;
using TMPro;
using HCB.PoolingSystem;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerVisual : SplineCharacter
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
    private bool _isGameFinished;
    

    [SerializeField] private ParticleSystem _sweatingParticle;
    [SerializeField] private float _headChangeSpeed;

    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance.OnStageSuccess.AddListener(OnLevelFinish); //LevelManager ile degistirdik, bu hemen cagriliyor. Level Manager bolum degistikten sonra.
        GameManager.Instance.OnStageFail.AddListener(OnLevelFinish);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameManager.Instance.OnStageSuccess.RemoveListener(OnLevelFinish);
        GameManager.Instance.OnStageFail.RemoveListener(OnLevelFinish);

    }

    void TiredMaterial()
    {
        if (_isGameFinished)
            return;
        
        _normalizeStamina = NormalizeValue(Stamina.CurrentStamina, 0, Stamina.MaxStamina); // bunu tam anlamadim

        SkinnedMeshRenderer.material.SetFloat("_Postion", _normalizeStamina);



        if(Stamina.CurrentStamina < Stamina.MaxStamina / 7) // hotfix (sonra duzeltilsin)
        {
            Sweat();
            SkinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.Clamp(Mathf.Sin(Time.time * _headChangeSpeed) * 100, 0, 100));

            Events.OnStaminaLow.Invoke();
            HapticManager.Haptic(HapticTypes.RigidImpact); // sor
        }
        else
        {
            StopSweat();
            SkinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.Lerp(SkinnedMeshRenderer.GetBlendShapeWeight(0), 0, Time.deltaTime * _headChangeSpeed));

            Events.OnStaminaNormal.Invoke();
            
        }
    }

    private void Update()
    {
        TiredMaterial();

        CheckDistanceTravelled();

        Debug.Log("NormalizeStamina: " + _normalizeStamina );
        Debug.Log("CurrentStamina: " + Stamina.CurrentStamina);
        Debug.Log("MaxStamina: " + Stamina.MaxStamina);


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

            GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] += (float)IncomeManager.IdleStat.CurrentValue;

            HCB.Core.EventManager.OnPlayerDataChange.Invoke();
            EventManager.OnMoneyEarned.Invoke();
            CreateFloatingText("+" + IncomeManager.IdleStat.CurrentValue.ToString("N1") + " $", Color.green, 1f);
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

    void OnLevelFinish()
    {
        _isGameFinished = true;
        StopSweat();
       
    }

}