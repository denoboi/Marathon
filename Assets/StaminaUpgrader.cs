using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.IncrimantalIdleSystem;

public class StaminaUpgrader : IdleStatObjectBase
{
    Stamina _stamina;

    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }


    public override void UpdateStat(string id)
    {
        Stamina.CurrentStamina = (float)IdleStat.CurrentValue;
    }
}
