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
        
    }

    void Move()
    {

        if (_isDead)
            return;


        if (Input.GetMouseButtonDown(0))
        {
            _speed += 0.03f;
            Animator.SetTrigger("Run");
            //time delta time ile carpmaya gerek yok button olsaydi ya da update olsaydi gerekebilirdi
            Stamina.StaminaTween(Stamina.CurrentStamina -3f);

            if(Stamina.CurrentStamina <= 0)
            {
                _isDead = true;
                GetComponent<RagdollController>().EnableRagdollWithForce(Vector3.forward, 100f); //sahneyi yanlis kurmusum once forward sonra back olmaliydi
                Run.After(0.75f, () => GetComponent<RagdollController>().EnableRagdollWithForce(Vector3.back, 860f));
                

            }

        }

        SlowDown();

    }

    void SlowDown()
    {
        _speed -= 0.04f * Time.deltaTime;
        Animator.SetFloat("Speed", _speed);

        if (_speed <= 0)
            _speed = 0;

        
        Stamina.StaminaRegen();
    }

}
