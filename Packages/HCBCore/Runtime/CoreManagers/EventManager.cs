using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using HCB.Utilities;

namespace HCB.Core
{
    public static class EventManager
    {
        public static UnityEvent OnPlayerDataChange = new UnityEvent();
        public static CurrencyEvent OnCurrencyInteracted = new CurrencyEvent();

        public static UnityEvent OnMoneyEarned = new UnityEvent();
        public static UnityEvent OnCountDownEnd = new UnityEvent();
        public static UnityEvent OnCountDownReady = new UnityEvent();
        public static UnityEvent OnCountDownSet = new UnityEvent();
        public static UnityEvent OnGunShoot = new UnityEvent();

        public static StringEvet OnStatUpdated = new StringEvet();
        #region Editor
        public static UnityEvent OnLevelDataChange = new UnityEvent();
        #endregion
    }

    public class PlayerDataEvent : UnityEvent<PlayerData> { }
    public class CurrencyEvent : UnityEvent<ExchangeType, float> { }

    public class StringEvet : UnityEvent<string> { }
}