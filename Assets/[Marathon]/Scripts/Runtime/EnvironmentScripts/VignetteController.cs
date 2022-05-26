using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

public class VignetteController : MonoBehaviour
{
    private Q_Vignette_Single vignette;
    public Q_Vignette_Single Vignette { get { return vignette == null ? vignette = GetComponent<Q_Vignette_Single>() : vignette; } }

    [SerializeField] private float vignetteSpeed;

    private bool canVignette = false;
    private void OnEnable()
    {
        if (Managers.Instance == null) return;

        LevelManager.Instance.OnLevelFinish.AddListener(ResetVignette);
        Events.OnStaminaLow.AddListener(() => canVignette = true);
        Events.OnStaminaNormal.AddListener(ResetVignette);
    }
    private void OnDisable()
    {
        if (Managers.Instance == null) return;

        LevelManager.Instance.OnLevelFinish.RemoveListener(ResetVignette);
        Events.OnStaminaLow.RemoveListener(() => canVignette = true);
        Events.OnStaminaNormal.RemoveListener(ResetVignette);
    }
    private void Update()
    {
        OnLowStamina();
    }
    private void ResetVignette()
    {
        canVignette = false;
        Vignette.mainScale = 0;
    }


    private void OnLowStamina()
    {
        if (!canVignette)
            return;

        Vignette.mainScale = Mathf.Clamp(Mathf.Sin(Time.time * vignetteSpeed) * 1.2f, 0, 100);
    }
}
