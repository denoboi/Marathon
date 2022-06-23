using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillController : MonoBehaviour
{
    private Animator _animator;
    private Stamina _stamina;

    public Animator Animator { get { return _animator == null ?  _animator = GetComponent<Animator>() : _animator; } }
    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }

     public float Speed;
    private bool _isDead;
    

    void Update()
    {
        Move();
        SlowDown(); //update'de event invokelama inayi

    }

    void Move()
    {

        if (_isDead)
            return;


        if (Input.GetMouseButton(0))
        {
            Speed += 0.2f * Time.deltaTime;
            Animator.SetTrigger("Run");
            //time delta time ile carpmaya gerek yok button olsaydi ya da update olsaydi gerekebilirdi
            Stamina.StaminaTween(Stamina.CurrentStamina - 5f);
            
            if(Stamina.CurrentStamina <= 20)
            {
                Events.OnShaking.Invoke();
            }


            if (Stamina.CurrentStamina <= 0)
            {
                _isDead = true;

                GetComponent<RagdollController>().EnableRagdollWithForce(Vector3.forward, 100);
                Run.After(0.1f, () => Events.OnPlayerFall.Invoke());
 
            }
            
        }

    }

    void SlowDown()
    {

            Speed -= 0.05f * Time.deltaTime;
            Animator.SetFloat("Speed", Speed);

            if (Speed <= 0)
                Speed = 0;

            Stamina.StaminaRegen();

    }

}
