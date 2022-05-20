using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HCB.IncrimantalIdleSystem.Examples
{
    public class IdleUIObject : IdleStatObjectBase
    {
        TextMeshProUGUI text;
        TextMeshProUGUI Text { get { return (text == null) ? text = GetComponent<TextMeshProUGUI>() : text; } }

        public override void Initialize()
        {
            base.Initialize();
            Text.SetText(IdleStat.StatID + ": " + IdleStat.CurrentValue.ToString("N0"));
        }

        public override void UpdateStat(string id)
        {
            Text.SetText(IdleStat.StatID + ": " + IdleStat.CurrentValue.ToString("N0"));
        }
    }
}
