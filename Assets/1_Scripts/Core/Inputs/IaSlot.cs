using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Cf.Inputs
{
    [Serializable]
    public struct IaSlotData
    {
        public int idx;
        public string bindKeyboard;
        [Space]
        public bool isClick;
    }

    public class IaSlot : IaBase<IaSlotData>
    {
        [SerializeField] private IaSlotData iaSlotData;

        public void SetIdx(int idx)
        {
            iaSlotData.idx = idx;
        }

        public void SetBindKeyboard(string key)
        {
            iaSlotData.bindKeyboard = key;
        }
        
        public override event Action<IaSlotData> OnInput;

        protected override void SetEventCondition(ref IaEventCondition condition)
        {
            
        }

        protected override void AddBinding(ref InputAction inputAction)
        {
            inputAction.AddBinding($"<Keyboard>/{iaSlotData.bindKeyboard}");
        }

        protected override void OnCallback(InputAction.CallbackContext callbackContext)
        {
            iaSlotData.isClick = callbackContext.ReadValue<float>() > 0;
            
            OnInput?.Invoke(iaSlotData);
        }
    }
}
