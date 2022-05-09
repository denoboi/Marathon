using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.SplineMovementSystem.Samples
{
    public class TrailController : MonoBehaviour
    {
        SplineCharacter _splineCharacter;
        SplineCharacter SplineCharacter => _splineCharacter == null ? _splineCharacter = GetComponentInParent<SplineCharacter>() : _splineCharacter;
                
        public List<TrailRenderer> Trails = new List<TrailRenderer>();
        public List<ParticleSystem> Particles = new List<ParticleSystem>();

        private void Awake()
        {
            StopEmitting();
        }

        private void OnEnable()
        {
            SplineCharacter.OnSlideStart.AddListener(StartEmitting);
            SplineCharacter.OnSlideStop.AddListener(StopEmitting);
        }

        private void OnDisable()
        {
            SplineCharacter.OnSlideStart.RemoveListener(StartEmitting);
            SplineCharacter.OnSlideStop.RemoveListener(StopEmitting);
        }        
        private void StartEmitting()
        {
            SetParticlesEmission(true);
            SetTrailsRendererEmitting(true);
        }
        private void StopEmitting() 
        {
            SetParticlesEmission(false);
            SetTrailsRendererEmitting(false);
        }
        private void SetParticlesEmission(bool enabled)
        {
            foreach (var particle in Particles)
            {
                var emission = particle.emission;
                emission.enabled = enabled;
            }
        }
        private void SetTrailsRendererEmitting(bool emitting) 
        {
            foreach (var trail in Trails)
            {
                trail.emitting = emitting;
            }
        }
    }
}
