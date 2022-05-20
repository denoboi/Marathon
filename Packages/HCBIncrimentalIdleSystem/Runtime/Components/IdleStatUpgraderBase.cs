using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using Sirenix.OdinInspector;


namespace HCB.IncrimantalIdleSystem
{
    public class IdleStatUpgraderBase : IdleStatBase, IStatUpgrader
    {

        private void Start()
        {
            Initialize();
        }

        public virtual void UpgradeStat()
        {
            IdleStat.Level++;
            EventManager.OnStatUpdated.Invoke(IdleStat.StatID);
        }
    }
}
