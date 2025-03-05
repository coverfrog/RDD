using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cf.Inputs
{
    public class IaRightClick : IaBase<bool>
    {
        public override event Action<bool> OnInput;

        protected override void SetEventCondition(ref IaEventCondition condition)
        {
            
        }

        protected override void AddBinding(ref InputAction inputAction)
        {
            inputAction.AddBinding("<Mouse>/rightButton");
        }

        protected override void OnCallback(InputAction.CallbackContext callbackContext)
        {
            bool isClick = callbackContext.ReadValue<float>() > 0;
            
            OnInput?.Invoke(isClick);
        }
    }
}
