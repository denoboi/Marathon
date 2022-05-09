using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.WeaponSystem
{
    public interface IWeapon
    {
        WeaponData WeaponData { get; set; }
        void Shoot(ITarget self, ITarget target);
        void Reload();
    }
}
