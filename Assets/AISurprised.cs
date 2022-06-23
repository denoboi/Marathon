using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISurprised : MonoBehaviour
{
    private Animator _animator;
    public Animator Animator { get { return _animator == null ?  _animator = GetComponent<Animator>() : _animator; } }

    // Update is called once per frame
    void Update()
    {
        SurpriseAnimation();
        Surprise();
    }

    void SurpriseAnimation()
    {
        Animator.SetTrigger("Surprised");
    }

    void Surprise()
    {
        Animator.SetTrigger("Wow");
    }


}
