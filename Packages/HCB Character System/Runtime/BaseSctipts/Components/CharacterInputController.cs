using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;

namespace HCB.CharacterSystem
{
    public class CharacterInputController : MonoBehaviour
    {

        System.Type interfaceType;


        private ICharacterBrain characterBrain;
        public ICharacterBrain CharacterBrain { get { return HCBUtilities.IsNullOrDestroyed(characterBrain, out interfaceType) ? characterBrain = GetComponent<ICharacterBrain>() : characterBrain; } }

        void Update()
        {
            if (CharacterBrain == null)
                return;

            CharacterBrain.Logic();
        }

    }
}
