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

   

   


   

   
}
