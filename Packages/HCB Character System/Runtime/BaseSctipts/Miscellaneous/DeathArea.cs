using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.CharacterSystem;

namespace HCB
{
    public class DeathArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Character character = other.GetComponent<Character>();
            if (character != null)
            {
                character.KillCharacter();
            }
        }
    }
}
