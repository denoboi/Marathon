using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HCB.WeaponSystem.TurretDemo
{
    public class GuidedMissile : Bullet
    {
        public GameObject ExplosionEffect;

        protected override void Update()
        {
            base.Update();

            var lookPos = Target.t.position - transform.position;

            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            Instantiate(ExplosionEffect, collision.GetContact(0).point, Quaternion.LookRotation(transform.forward, collision.GetContact(0).normal));

        }
    }
}
