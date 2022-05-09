using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

namespace HCB.CollectableSystem.Examples
{
    public class SimpleCubeController : MonoBehaviour
    {
        Rigidbody rigidbody;
        public float speed;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            rigidbody.MovePosition(transform.position + InputManager.Instance.InputDirection * speed * Time.deltaTime);
        }
    }
}
