using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.CharacterSystem
{
    public interface ICharacterBrain : IComponent
    {
        MonoBehaviour MonoBehaviour { get; }

        ICharacterController CharacterController { get; }

        void Initialize();
        void Logic();
        void Dispose();

    }
}
