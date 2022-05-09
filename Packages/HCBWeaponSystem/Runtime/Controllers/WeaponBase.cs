using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace HCB.WeaponSystem
{
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {
        [SerializeField]
        private WeaponData weaponData;
        public WeaponData WeaponData { get { return weaponData; } set { weaponData = value; } }


        public GameObject BulletPrefab;
        public List<Transform> FirePoint = new List<Transform>();


        [Header("Current Data")]
        [ReadOnly]
        public int CurrentBulletInMagazine;

        private void Start()
        {
            CurrentBulletInMagazine = WeaponData.MagazineCapacity;
        }

        public virtual void Reload()
        {

            if (CurrentBulletInMagazine > 0)
                return;

            //Send Reload Message
            Run.After(3f, () => CurrentBulletInMagazine = WeaponData.MagazineCapacity);
        }

        protected float lastShootTime;

        [Button]
        public virtual void Shoot(ITarget self, ITarget target)
        {

            if (CurrentBulletInMagazine == 0)
                return;

            if (Time.time < lastShootTime + (WeaponData.FireRate))
                return;


            lastShootTime = Time.time;
            for (int i = 0; i < FirePoint.Count; i++)
            {
                GameObject bulletgo = Instantiate(BulletPrefab, FirePoint[i].transform.position, FirePoint[i].transform.rotation);
                Bullet bullet = bulletgo.GetComponent<Bullet>();
                bullet.Damage = WeaponData.DamagePerHit;
                bullet.InitBullet(self, target, WeaponData.DamagePerHit);

                if (WeaponData.MagazineCapacity == -1)
                    return;

                CurrentBulletInMagazine--;
                Reload();
            }
           
        }
    }
}
