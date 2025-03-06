using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cf.Inputs
{
    [Serializable]
    public class IaSetting
    {
        [SerializeField] private List<string> slotSkillKeyBoardList = new List<string>(4){ "q", "w", "e", "r"};

        public IEnumerable<string> SlotSkillKeyBoardList => slotSkillKeyBoardList.Select(k => k.ToLower());
    }
}
