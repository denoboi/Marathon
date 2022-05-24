using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.IncrimantalIdleSystem;
using Dreamteck.Forever;

public class SpeedUpgraderAI : IdleStatObjectBase
{
    private Runner _runner;

    public Runner Runner { get { return _runner == null ? _runner = GetComponent<Runner>() : _runner; } }

    public override void UpdateStat(string id)
    {
        Runner.followSpeed = (float)IdleStat.CurrentValue + Random.Range(-2, +3);
    }
}
