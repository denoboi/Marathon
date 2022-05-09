using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        private IAim aim;
        public IAim Aim { get { return (aim == null) ? aim = GetComponentInChildren<IAim>() : aim; } }

        private IWeapon weapon;
        public IWeapon Weapon { get { return (weapon == null) ? weapon = GetComponentInChildren<IWeapon>() : weapon; } }


        private void Update()
        {
            Aim.FindTargets();
        }
    }
}
