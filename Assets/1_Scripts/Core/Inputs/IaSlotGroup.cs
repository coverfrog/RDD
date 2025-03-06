using System;
using System.Collections.Generic;
using System.Linq;
using Cf.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cf.Inputs
{
    public class IaSlotGroup : MonoBehaviour
    {
        [SerializeField] private string mGroupName;
        [Space]
        [SerializeField] private List<IaSlot> mIaSlotList = new List<IaSlot>();

        public void Init(string groupName, IEnumerable<string> keyList)
        {
            mGroupName = groupName;
            
            int idx = 0;
            
            List<IaSlot> newIaSlotList = new List<IaSlot>();
            
            foreach (string key in keyList)
            {
                ComponentsUtil.TryAddComponent(this, out IaSlot iaSlot, true);
                
                iaSlot.SetIdx(idx);
                iaSlot.SetBindKeyboard(key);
                iaSlot.OnInput += IaSlotOnOnInput;
                
                newIaSlotList.Add(iaSlot);

                ++idx;
            }

            mIaSlotList = newIaSlotList;
        }

        public void Begin()
        {
            foreach (IaSlot iaSlot in mIaSlotList)
            {
                iaSlot.Begin();
            }
        }

        public event Action<int, bool> OnInput; 

        private void IaSlotOnOnInput(IaSlotData iaSlotData)
        {
            OnInput?.Invoke(iaSlotData.idx, iaSlotData.isClick);
        }
    }
}
