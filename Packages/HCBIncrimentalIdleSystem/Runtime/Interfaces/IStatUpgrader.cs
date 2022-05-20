using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.IncrimantalIdleSystem
{
    public interface IStatUpgrader : IStat
    {
        //Save your file after each upgrade with IdleData.SaveData();
        public void UpgradeStat();
    }
}
