using System;
using System.Collections.Generic;
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
        [SerializeField] private List<bool> isSkillSlotClickList = new List<bool>();

        private void GetIsSkillSlotClick(int idx)
        {
            if (idx >= isSkillSlotClickList.Count)
            {
                isSkillSlotClickList.AddRange(new bool[1 + idx - isSkillSlotClickList.Count]);
            }
        }

        public bool GetIsSkillSlotClickList(int idx)
        {
            GetIsSkillSlotClick(idx);

            return isSkillSlotClickList[idx];
        }

        public void SetIsSkillSlotClickList(int idx, bool b)
        {
            GetIsSkillSlotClick(idx);

            isSkillSlotClickList[idx] = b;
        }
    }
}
