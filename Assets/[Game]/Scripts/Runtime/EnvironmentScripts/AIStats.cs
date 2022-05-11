using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStats : MonoBehaviour
{
    private Stamina _stamina;

    private AIMovement _aiMovement;

    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }
    public AIMovement AIMovement { get { return _aiMovement == null ? _aiMovement = GetComponent<AIMovement>() : _aiMovement; }  }


    private bool IsAIStaminaRegenerate;

    private void Start()
    {
        
    }

    private void Update()
    {
        StaminaDrain();
        
    }



    void StaminaRegen()
    {

        if (!IsAIStaminaRegenerate)
            return;

        if (Stamina.AIStamina < Stamina._maxStamina)
        {
            Stamina.AIStamina += Time.deltaTime * Stamina._staminaRegenMultiplier;
            if (Stamina.AIStamina > Stamina._maxStamina)
                Stamina.AIStamina = Stamina._maxStamina;
        }

       
       
        


    }

    void StaminaDrain()
    {

        if (IsAIStaminaRegenerate)
            return;

        Stamina.AIStamina -= Time.deltaTime * Stamina._staminaDrainMultiplier;

       


        //if (Stamina.AIStamina <= 0)
        //{
        //    Stamina.AIStamina = 0f;
        //    Debug.Log("AIBoom");

        //}




    }
}
