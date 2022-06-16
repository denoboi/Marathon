using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillController : MonoBehaviour
{
    private Animator _animator;

    public Animator Animator { get { return _animator == null ?  _animator = GetComponent<Animator>() : _animator; } }

     private float _speed;

    void Update()
    {
        Run();
    }

    void Run()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _speed += 0.03f;
            Animator.SetTrigger("Run");
            //time delta time ile carpmaya gerek yok button olsaydi ya da update olsaydi gerekebilirdi
        }

        SlowDown();
       
    }

    void SlowDown()
    {
        _speed -= 0.04f * Time.deltaTime;
        Animator.SetFloat("Speed", _speed);

        if (_speed <= 0)
            _speed = 0;
    }

}
