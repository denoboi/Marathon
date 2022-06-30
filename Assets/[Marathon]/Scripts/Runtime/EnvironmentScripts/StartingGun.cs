using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

public class StartingGun : MonoBehaviour
{
    [SerializeField] ParticleSystem _flareParticle;

    private void OnEnable()
    {
        EventManager.OnGunShoot.AddListener(StartingGunParticle);
    }

    private void OnDisable()
    {
        EventManager.OnGunShoot.RemoveListener(StartingGunParticle);
    }


     void StartingGunParticle()
    {
        _flareParticle.Play();
    }
}
