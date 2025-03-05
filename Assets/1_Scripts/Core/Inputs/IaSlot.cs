using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cf.Inputs
{
    public abstract class IaSlot : IaBase<bool>
    {
        public abstract override event Action<bool> OnInput;
        
        protected abstract override void SetEventCondition(ref IaEventCondition condition);

        protected abstract override void AddBinding(ref InputAction inputAction);
        
        protected abstract override void OnCallback(InputAction.CallbackContext callbackContext);
    }
}
