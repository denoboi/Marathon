using HCB.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCPIVisual : MonoBehaviour
{
    private SkinnedMeshRenderer _playerMat;
    private Stamina _stamina;


    public SkinnedMeshRenderer SkinnedMeshRenderer { get { return _playerMat == null ? _playerMat = GetComponentInChildren<SkinnedMeshRenderer>() : _playerMat; } }
    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }


    [SerializeField] private ParticleSystem _sweatingParticle;
    [SerializeField] private float _headChangeSpeed;

    private float _normalizeStamina;


    private void Update()
    {
        TiredMaterial();
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
}
