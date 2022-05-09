using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HCB.WeaponSystem
{
    [System.Serializable]
    public class AimConstrains
    {
        public bool x, y, z;
    }

    public class AimController : MonoBehaviour
    {
        public AimConstrains AimConstrains;

        public void AimThowards(Vector3 targetPos)
        {
            var lookPos = targetPos - transform.position;

            lookPos.x = (AimConstrains.x == true) ? lookPos.x = 0 : lookPos.x;
            lookPos.y = (AimConstrains.y == true) ? lookPos.y = 0 : lookPos.y;
            lookPos.z = (AimConstrains.z == true) ? lookPos.z = 0 : lookPos.z;

            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }
    }
}
