using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.DamageSystem
{
    public class Damager : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.Damage(1);
            }
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDamagable damagable = collision.collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.Damage(1);
            }
            Destroy(gameObject);
        }
    }
}
