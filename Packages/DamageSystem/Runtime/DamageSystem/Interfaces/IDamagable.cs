using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

namespace HCB.DamageSystem
{
    public interface IDamagable : IComponent
    {
        public int MaxHitPoint { get; set; }
        public int CurrentHitPoint { get; set; }
        public int InitialDamage { get; set; }
        public bool IsDisposed { get; set; }

        void Damage(int damageAmount);
        void Dispose();
    }
}