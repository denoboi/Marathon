using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.SplineMovementSystem
{
    public interface IJumpTarget
    {
        void Jump(float jumpForce);
    }
}
