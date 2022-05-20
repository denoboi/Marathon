using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;
using Sirenix.OdinInspector;

namespace HCB.IncrimantalIdleSystem
{
    public abstract class IdleStatObjectBase : IdleStatBase, IStatObject
    {
        
        private void Start()
        {
            Initialize();
        }

        protected virtual void OnEnable()
        {
            EventManager.OnStatUpdated.AddListener(UpdateStat);
        }

        protected virtual void OnDisable()
        {
            EventManager.OnStatUpdated.RemoveListener(UpdateStat);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public abstract void UpdateStat(string id);
    }
}
