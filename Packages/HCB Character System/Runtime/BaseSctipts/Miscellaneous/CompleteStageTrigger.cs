using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using HCB.CharacterSystem;
using HCB.Core;

namespace HCB
{
    public class CompleteStageTrigger : MonoBehaviour
    {
        public bool OnEnter;
        public bool isSuccess;

        private void OnTriggerEnter(Collider other)
        {
            if (!OnEnter)
                return;

            Character character = other.GetComponent<Character>();
            if (character)
            {
                if (character.CharacterData.CharacterControlType == CharacterControlType.Player)
                    GameManager.Instance.CompeleteStage(isSuccess);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (OnEnter)
                return;

            Character character = other.GetComponent<Character>();
            if (character)
            {
                if (character.CharacterData.CharacterControlType == CharacterControlType.Player)
                    GameManager.Instance.CompeleteStage(isSuccess);
            }
        }
    }
}