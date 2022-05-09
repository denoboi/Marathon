using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.WeaponSystem.TurretDemo
{
    public class MissileLuncher : WeaponBase
    {
        public List<ParticleSystem> MuzzleEffect = new List<ParticleSystem>();

        public override void Shoot(ITarget self, ITarget target)
        {
            if (CurrentBulletInMagazine == 0)
                return;

            if (Time.time < lastShootTime + (WeaponData.FireRate))
                return;

            base.Shoot(self, target);

            foreach (var item in MuzzleEffect)
            {
                item.Play();
            }

        }
    }
}
