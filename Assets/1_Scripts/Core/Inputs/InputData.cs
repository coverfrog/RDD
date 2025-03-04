using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cf.Inputs
{
    [Serializable]
    public class InputData
    {
        public bool isRightClick;
        
        public Vector3 moveDirectionVector3;
        public Vector2 rightClickVector2;
    }
}
