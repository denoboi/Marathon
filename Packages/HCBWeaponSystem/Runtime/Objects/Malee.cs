using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.WeaponSystem
{
    public class Malee : WeaponBase
    {
        public override void Shoot(ITarget self, ITarget target)
        {
            if (Time.time < lastShootTime + WeaponData.FireRate)
                return;

            base.Shoot(self, target);
        }
    }
}
