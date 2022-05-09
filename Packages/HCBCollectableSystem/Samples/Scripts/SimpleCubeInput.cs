using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

namespace HCB.CollectableSystem.Examples
{
    public class SimpleCubeInput : MonoBehaviour
    {
        private void Update()
        {
            InputManager.Instance.InputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}
