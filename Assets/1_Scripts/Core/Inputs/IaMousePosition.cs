using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cf.Inputs
{
    public class IaMousePosition : IaBase<Vector2>
    {
        public override event Action<Vector2> OnInput;

        protected override void SetEventCondition(ref IaEventCondition condition)
        {
            
        }

        protected override void AddBinding(ref InputAction inputAction)
        {
            inputAction.AddBinding("<Mouse>/position");
        }

        protected override void OnCallback(InputAction.CallbackContext callbackContext)
        {
            Vector2 pos = callbackContext.ReadValue<Vector2>();

            float clampedX = Mathf.Clamp(pos.x, 0, Screen.width);
            float clampedY = Mathf.Clamp(pos.y, 0, Screen.height);

            OnInput?.Invoke(new Vector2(clampedX, clampedY));
        }
    }
}
