using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace HCB.IncrimantalIdleSystem
{
    [System.Serializable]
    public class IdleStat : ScriptableObject
    {
        [Header("Setup @Dev")]
        public string StatID;

        [Header("Settings @Design")]
        [BoxGroup("StatValues")]
        public float InitialValue = 1;

        [BoxGroup("StatValues")]
        [PropertyRange(0.1f, 2)]
        public float UpgradeMultiplier = 1.2f;

        [BoxGroup("CostValues")]
        public float InitialCost = 40;

        [BoxGroup("CostValues")]
        [PropertyRange(1f, 1.9f)]
        public float CostMultiplier = 1.1f;

        [BoxGroup("CostValues")]
        public ExchangeType ExchangeType = ExchangeType.Coin;

        [ShowInInspector]
        public int Level { get { return PlayerPrefs.GetInt(StatID, 0); } set { PlayerPrefs.SetInt(StatID, value); } }

        [BoxGroup("CostValues")]
        [ShowInInspector]
        [ReadOnly]
        public double CurrentCost { get { return Mathf.RoundToInt(InitialCost * Mathf.Pow(CostMultiplier, Level)); } }


        [BoxGroup("StatValues")]
        [ShowInInspector]
        [ReadOnly]
        public double CurrentValue { get { return (Level * UpgradeMultiplier) + InitialValue; } }
    }
}
