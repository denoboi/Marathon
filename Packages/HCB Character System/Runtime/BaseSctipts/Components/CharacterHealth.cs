using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using HCB.DamageSystem;

namespace HCB.CharacterSystem
{
    public class CharacterHealth : InterfaceBase, IDamagable
    {
        Character character;
        Character Character { get { return (character == null) ? character = GetComponentInParent<Character>() : character; } }

        public int MaxHitPoint { get { return Character.CharacterData.CharacterHealthData.MaxHealth; } set => throw new System.NotImplementedException(); }

        private int currentHitPoint = -1;
        public int CurrentHitPoint { get { return (currentHitPoint < 0) ? currentHitPoint = MaxHitPoint : currentHitPoint; } set => currentHitPoint = value; }
        private int initialDamage = 1;
        public int InitialDamage { get { return initialDamage; } set => initialDamage = value; }

        private bool isDisposed = false;
        public bool IsDisposed { get => isDisposed; set => isDisposed = value; }

        private void Start()
        {
            Initilize();
        }

        private void OnEnable()
        {
            Character.OnCharacterRevive.AddListener(Initilize);
            Character.OnCharacterReciveDamage.AddListener(Damage);
        }

        private void OnDisable()
        {
            Character.OnCharacterRevive.RemoveListener(Initilize);
            Character.OnCharacterReciveDamage.RemoveListener(Damage);
        }

        private void Initilize()
        {
        }

        public void Damage(int damageAmount)
        {
            Character.IsControlable = false;
            Run.After(1f, () =>
            {
                Character.IsControlable = !Character.isDead;
            });
            CurrentHitPoint -= damageAmount;
            if (CurrentHitPoint <= 0)
                Dispose();
            else Character.OnCharacterHit.Invoke();
        }

        public void Dispose()
        {
            Character.KillCharacter();
        }
    }
}
