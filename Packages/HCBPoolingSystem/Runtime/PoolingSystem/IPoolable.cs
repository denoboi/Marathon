using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HCB.PoolingSystem
{
    public interface IPoolable
    {
        void Initilize();

        void Dispose();
    }
}