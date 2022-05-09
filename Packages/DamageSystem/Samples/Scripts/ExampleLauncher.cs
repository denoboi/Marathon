using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.DamageSystem.Examples
{
    public class ExampleLauncher : MonoBehaviour
    {
        public GameObject ProjectilePrefab;
        public Transform FirePoint;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                Instantiate(ProjectilePrefab, FirePoint.position, FirePoint.rotation);
            }
        }
    }
}
