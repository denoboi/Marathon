using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.CollectableSystem;
using HCB.Core;

namespace HCB.CollectableSystem.Examples
{
    public class ExampleCollectable : CollectableBase
    {
        public ExchangeType ExchangeType;
        public int Amount;

        public override void Collect(Collector collector)
        {
            base.Collect(collector);
            EventManager.OnCurrencyInteracted.Invoke(ExchangeType, Amount);
            Destroy(gameObject);
        }
    }
}
