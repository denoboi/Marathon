using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.IncrimantalIdleSystem;


[CreateAssetMenu(fileName = "New Stat", menuName = "Stats/Income")]

public class IncomeManager : IdleStatObjectBase
{

    private float _incomeRate;

    public override void UpdateStat(string id)
    {
        _incomeRate = (float) IdleStat.CurrentValue;
    }
}
   
