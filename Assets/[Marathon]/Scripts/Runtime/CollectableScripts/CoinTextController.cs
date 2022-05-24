using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using TMPro;

public class CoinTextController : MonoBehaviour
{
    TextMeshProUGUI _coinText;

    TextMeshProUGUI CoinText { get { return _coinText == null ? _coinText = GetComponent<TextMeshProUGUI>() : _coinText; } }


    private void OnEnable()
    {
        HCB.Core.EventManager.OnPlayerDataChange.AddListener(UpdateText);

        SceneController.Instance.OnSceneLoaded.AddListener(UpdateText);

        
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        HCB.Core.EventManager.OnPlayerDataChange.RemoveListener(UpdateText);

        SceneController.Instance.OnSceneLoaded.RemoveListener(UpdateText);
    }


    void UpdateText()
    {
        CoinText.text = GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin].ToString();
        
    }

}
