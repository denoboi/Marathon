using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

public class Treadmill : MonoBehaviour
{
    private MeshRenderer _renderer;
    private Animator _animator;

    public Animator Animator { get { return _animator == null ? _animator = GetComponent<Animator>() : _animator; } }

    public MeshRenderer Renderer { get { return _renderer == null ? _renderer = GetComponent<MeshRenderer>() : _renderer; } }

    public TreadmillController TreadmillController;

    private float _offset;
    

    [SerializeField] private ParticleSystem _brokenParticle;
    [SerializeField] private ParticleSystem _fireParticle1;
    [SerializeField] private ParticleSystem _fireParticle2;
    [SerializeField] private ParticleSystem _fireParticle3;
    [SerializeField] private ParticleSystem _fireParticle4;


    private void OnEnable()
    {
       
        Events.OnShaking.AddListener(Shaking);
    }

    private void OnDisable()
    {
        Events.OnShaking.RemoveListener(Shaking);
    }


    private void Update()
    {
        MaterialOffsetPositive();
        Shaking();
        StopShaking();
    }

    void MaterialOffsetPositive()
    {
        

        _offset += Time.deltaTime * TreadmillController.Speed ;
        Renderer.materials[1].mainTextureOffset = new Vector2(_offset,0);


    }



    void Shaking()
    {
        if(TreadmillController.Stamina.CurrentStamina <= 20)
        {
            Animator.SetBool("Shake", true) ;
            var emission = _brokenParticle.emission;
            emission.rateOverTime = 30f;
            emission = _fireParticle1.emission;
            emission.rateOverTime = 30f;
            emission = _fireParticle2.emission;
            emission.rateOverTime = 30f;
            emission = _fireParticle3.emission;
            emission.rateOverTime = 30f;
            emission = _fireParticle4.emission;
            emission.rateOverTime = 30f;

        }

       
        
    }

    void StopShaking()
    {

        if(TreadmillController.Stamina.CurrentStamina >= 20)
        {
            Animator.SetBool("Shake",false);
            var emission = _brokenParticle.emission;
            emission.rateOverTime = 0f;
            emission = _fireParticle1.emission;
            emission.rateOverTime = 0f;
            emission = _fireParticle2.emission;
            emission.rateOverTime = 0f;
            emission = _fireParticle3.emission;
            emission.rateOverTime = 0f;
            emission = _fireParticle4.emission;
            emission.rateOverTime = 0f;
        }
       
    }
}
