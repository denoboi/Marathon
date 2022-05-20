using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.IncrimantalIdleSystem
{
    public interface IStatObject : IStat
    {
        public void UpdateStat(string id);
    }
}
