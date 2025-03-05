using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cf.Inputs
{
    [Serializable]
    public class IaData
    {
        public bool isLeftClick;
        public bool isRightClick;
        [Space] 
        public bool isSlot0Click;
        public bool isSlot1Click;
        public bool isSlot2Click;
        public bool isSlot3Click;
        [Space]
        public Vector3 moveDirectionVector3;
        public Vector2 mousePoint;
    }
}
