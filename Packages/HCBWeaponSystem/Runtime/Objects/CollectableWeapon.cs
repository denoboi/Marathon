using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.CollectableSystem;

namespace HCB.WeaponSystem
{
    public class CollectableWeapon : CollectableBase
    {
        public WeaponData WeaponData;

        public override void Collect(Collector collector)
        {
            base.Collect(collector);
            //Equip Weapon Here
            Destroy(gameObject);
        }
    }
}
