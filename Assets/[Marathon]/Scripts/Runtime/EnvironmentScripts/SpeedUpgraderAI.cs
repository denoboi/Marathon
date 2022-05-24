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
        Runner.followSpeed = Random.Range((float)IdleStat.CurrentValue - 2.5f, (float)IdleStat.CurrentValue + 0.5f);
    }
}
