using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.WeaponSystem
{
    
    public class AimBase : MonoBehaviour, IAim
    {
        private IWeapon weapon;
        public IWeapon Weapon { get { return (weapon == null) ? weapon = GetComponentInChildren<IWeapon>() : weapon; } }


        private ITarget selfTarget;
        public ITarget SelfTarget { get { return (selfTarget == null) ? selfTarget = GetComponentInChildren<ITarget>() : selfTarget; } }

        private ITarget target;

        private List<AimController> aimControllers;
        public List<AimController> AimControllers { get { return (aimControllers == null || aimControllers.Count == 0)
                    ? aimControllers = new List<AimController>(GetComponentsInChildren<AimController>()) : aimControllers; } }

        public virtual void FindTargets()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, Weapon.WeaponData.Range);
            float minDistance = Mathf.Infinity;
            bool targetFound = false;

            foreach (var item in colliders)
            {
                ITarget currentTarget = item.GetComponent<ITarget>();

                if (!ReferenceEquals(currentTarget, SelfTarget))
                {
                    if (currentTarget != null)
                    {
                        float distance = Vector3.Distance(transform.position, currentTarget.t.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            target = currentTarget;
                            targetFound = true;
                            Aim();
                        }
                    }
                }
            }

            if (!targetFound)
                target = null;
        }
        public void Aim()
        {
            foreach (var aimController in AimControllers)
            {
                aimController.AimThowards(target.t.position);
            }

            Weapon.Shoot(SelfTarget, target);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, Weapon.WeaponData.Range);
        }
    }
}