using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.DamageSystem.Examples
{
    public class ExampleProjectile : MonoBehaviour
    {
        Rigidbody rigidbody;
        public float speed;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            Destroy(gameObject, 5f);
        }

        private void FixedUpdate()
        {
            rigidbody.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        }
    }
}
