using System;
using System.Collections.Generic;
using Cf.Structs;
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
        public Vector3 moveDirectionVector3;
        public Vector2 mousePoint;
        [Space] 
        public AutoIncreaseList<bool> isSkillSlotClickList = new AutoIncreaseList<bool>();
    }
}
