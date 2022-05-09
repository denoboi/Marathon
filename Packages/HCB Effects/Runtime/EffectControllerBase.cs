using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using HCB.Core;

namespace HCB.Effects
{
    public abstract class EffectControllerBase : MonoBehaviour
    {
        private ParticleSystem particleSystem;
        public ParticleSystem ParticleSystem { get { return (particleSystem == null) ? particleSystem = GetComponent<ParticleSystem>() : particleSystem; } }


        protected virtual void PlayParticleEffect()
        {
            ParticleSystem.Play();
        }
    }
}


