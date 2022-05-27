using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.IncrimantalIdleSystem;

public class StaminaUpgraderAI : IdleStatObjectBase
{
    private Stamina _stamina;

    public Stamina Stamina { get { return _stamina ==null ? _stamina = GetComponent<Stamina>(): _stamina; } }
    public override void UpdateStat(string id)
    {
        Stamina.CurrentStamina = (float)IdleStat.CurrentValue + Random.Range(-5f, 0f);
        Stamina.MaxStamina = Stamina.CurrentStamina;
    }
}
