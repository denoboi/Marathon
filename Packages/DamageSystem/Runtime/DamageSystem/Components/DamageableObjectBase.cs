using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.DamageSystem
{
    public abstract class DamageableObjectBase : InterfaceBase, IDamagable
    {
        [SerializeField]
        private int maxHitPoint = 10;
        public int MaxHitPoint { get => maxHitPoint; set => maxHitPoint = value; }
        private int currentHitPoint = -1;
        public int CurrentHitPoint { get { return currentHitPoint; } set => currentHitPoint = value; }

        [SerializeField]
        private int initialDamage = 1;
        public int InitialDamage { get => initialDamage; set => initialDamage = value; }

        private bool isDisposed;
        public bool IsDisposed { get => isDisposed; set => isDisposed = value; }

        protected virtual void Start()
        {
            CurrentHitPoint = MaxHitPoint;
        }

        public virtual void Damage(int damageAmount)
        {
            if (isDisposed) return;

            CurrentHitPoint -= damageAmount;
            if (CurrentHitPoint <= 0)
                Dispose();
        }

        public virtual void Dispose()
        {
            IsDisposed = true;
            Destroy(gameObject);
        }
    }
}
