using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB;

namespace HCB.CameraSystem
{
    public interface ICameraTarget : IComponent
    {
        Transform transform { get; }
        void SubToCamera();
        void UnSubToCamera();
    }
}
