using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using HCB.Core;
using Newtonsoft.Json;
using System.IO;

namespace HCB.IncrimantalIdleSystem
{
    public class IdleStatBase : MonoBehaviour, IStat
    {
        [SerializeField]
        private IdleStat idleStat;
        public IdleStat IdleStat { get { return idleStat; } set { idleStat = value; } }


        public virtual void Initialize()
        {
            IdleStat = Instantiate(IdleStat);
        }
    }
}
