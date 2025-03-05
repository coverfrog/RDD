using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cf.Inputs
{
    [Serializable]
    public class InputData
    {
        public bool isLeftClick;
        public bool isRightClick;
        [Space] 
        public bool isSlot0Click;
        [Space]
        public Vector3 moveDirectionVector3;
        public Vector2 mousePoint;
    }
}
