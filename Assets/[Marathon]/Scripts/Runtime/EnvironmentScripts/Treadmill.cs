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
    }

    void MaterialOffsetPositive()
    {
        

        _offset += Time.deltaTime * TreadmillController.Speed ;
        Renderer.materials[1].mainTextureOffset = new Vector2(_offset,0);


    }

   

    void Shaking()
    {
        Animator.SetTrigger("Shake");
    }
}
