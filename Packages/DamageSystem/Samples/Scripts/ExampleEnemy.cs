using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.DamageSystem;
using DG.Tweening;

namespace HCB.DamageSystem.Examples
{
    public class ExampleEnemy : DamageableObjectBase
    {
        public override void Damage(int damageAmount)
        {
            base.Damage(damageAmount);
            transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);
        }
    }
}
