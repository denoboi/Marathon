using HCB.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HCB.PoolingSystem;
using DG.Tweening;




public class PlayerCPIVisual : MonoBehaviour
{
    private SkinnedMeshRenderer _playerMat;
    private Stamina _stamina;
    public TreadmillController TreadmillController;
    private float timer;


    public SkinnedMeshRenderer SkinnedMeshRenderer { get { return _playerMat == null ? _playerMat = GetComponentInChildren<SkinnedMeshRenderer>() : _playerMat; } }
    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }


    [SerializeField] private ParticleSystem _sweatingParticle;
    [SerializeField] private float _headChangeSpeed;
    [SerializeField] Transform StaminaTextPos;

    private float _normalizeStamina;

    

    private void Update()
    {
        TiredMaterial();



        if(TreadmillController.Speed > 2 && TreadmillController.Speed < 3)
        {
            timer += Time.deltaTime;

            if(timer > 0.5f)
            {
                timer = 0;
                CreateFloatingText("+" + "Stamina", Color.green, 1f);
            }
            
            
        }
    }


    void TiredMaterial()
    {
       
        _normalizeStamina = NormalizeValue(Stamina.CurrentStamina, 0, Stamina.MaxStamina); // bunu tam anlamadim

        SkinnedMeshRenderer.material.SetFloat("_Postion", _normalizeStamina);



        if (Stamina.CurrentStamina < Stamina.MaxStamina / 7) // hotfix (sonra duzeltilsin)
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


    private float NormalizeValue(float value, float min, float max)
    {
        float normalizedValue = (value - min) / (max - min);

        value = Mathf.Clamp(value, min, max);

        normalizedValue = Mathf.Clamp(normalizedValue, 0, 1);

        return normalizedValue;
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

    public void CreateFloatingText(string s, Color color, float delay)
    {

        Vector3 offset = new Vector3(1f, -1f, 0);
        TextMeshPro text = PoolingSystem.Instance.InstantiateAPS("staminaText", StaminaTextPos.position + offset).GetComponent<TextMeshPro>();
        text.transform.LookAt(Camera.main.transform);
        text.SetText(s);
        text.DOFade(1, 0);
        text.color = color;
        text.transform.DOMoveY(text.transform.position.y + 1f, delay);
        text.DOFade(0, delay / 2)
            .SetDelay(delay / 2)
            .OnComplete(() => PoolingSystem.Instance.DestroyAPS(text.gameObject));
    }
}
