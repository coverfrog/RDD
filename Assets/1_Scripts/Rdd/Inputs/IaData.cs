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
        public bool isSkillSlot0Click;
        public bool isSkillSlot1Click;
        public bool isSkillSlot2Click;
        public bool isSkillSlot3Click;
        [Space]
        public Vector3 moveDirectionVector3;
        public Vector2 mousePoint;
    }
}
