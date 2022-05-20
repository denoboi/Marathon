using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.IncrimantalIdleSystem
{
    public interface IStat 
    {
        public IdleStat IdleStat { get; set; }

        /// <summary>
        /// Call this at Start
        /// </summary>
        public void Initialize();
    }
}
