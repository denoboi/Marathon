using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using HCB.Utilities;

namespace HCB.CharacterSystem
{
    public abstract class CharacterBrainBase : InterfaceBase, ICharacterBrain
    {

        public MonoBehaviour MonoBehaviour { get { return this; } }

        System.Type interfaceType;

        private Character character;
        public Character Character { get { return (character == null) ? character = GetComponentInParent<Character>() : character; } }

        private ICharacterController characterController;
        public ICharacterController CharacterController { get { return HCBUtilities.IsNullOrDestroyed(characterController, out interfaceType) ? characterController = GetComponent<ICharacterController>() : characterController; } }

        public abstract void Logic();

        public virtual void Initialize()
        {
            //Debug.Log("Brain Intialize" + this.GetType().ToString());
        }

        public virtual void Dispose()
        {
            //Debug.Log("Brain Disposed" + this.GetType().ToString());
            HCBUtilities.DestroyExtended(this);
        }
    }
}
