using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

namespace HCB.CollectableSystem.Examples
{
    public class ExampleCollectedText : MonoBehaviour
    {
        //Normally this should be stored somewhere more accesable like Player Data
        private int currentCollectedAmount;

        private TextMesh textMesh;

        private void Start()
        {
            textMesh = GetComponent<TextMesh>();
        }

        private void OnEnable()
        {
            EventManager.OnCurrencyInteracted.AddListener(UpdateText);
        }

        private void OnDisable()
        {
            EventManager.OnCurrencyInteracted.RemoveListener(UpdateText);
        }

        private void UpdateText(ExchangeType exchangeType, int amount)
        {
            if(exchangeType == ExchangeType.Coin)
            {
                currentCollectedAmount++;
            }

            textMesh.text = "Collected: " + currentCollectedAmount;
        }
    }
}
