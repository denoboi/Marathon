using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillController : MonoBehaviour
{
    private Animator _animator;
    private Stamina _stamina;

    public Animator Animator { get { return _animator == null ?  _animator = GetComponent<Animator>() : _animator; } }
    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }

     private float _speed;
    private bool _isDead;

    void Update()
    {
        Move();
        SlowDown();

    }

    void Move()
    {

        if (_isDead)
            return;


        if (Input.GetMouseButtonDown(0))
        {
            _speed += 0.1f;
            Animator.SetTrigger("Run");
            //time delta time ile carpmaya gerek yok button olsaydi ya da update olsaydi gerekebilirdi
            Stamina.StaminaTween(Stamina.CurrentStamina - 10f);
            



            if (Stamina.CurrentStamina <= 0)
            {
                _isDead = true;

                GetComponent<RagdollController>().EnableRagdollWithForce(Vector3.forward, 100);
                Run.After(0.1f, () => Events.OnPlayerFall.Invoke());
                

            }

        }



        Events.OnOffsetPositive.Invoke();



    }

    void SlowDown()
    {
        _speed -= 0.04f * Time.deltaTime;
        Animator.SetFloat("Speed", _speed);

        if (_speed <= 0)
            _speed = 0;

        Events.OnOffsetNegative.Invoke();
        Stamina.StaminaRegen();
    }

}
