using System;
using UnityEngine;
using Cf.Inputs;
using UnityEngine.InputSystem;

public class IaSlot1 : IaSlot
{
    public override event Action<bool> OnInput;

    protected override void SetEventCondition(ref IaEventCondition condition)
    {
            
    }

    protected override void AddBinding(ref InputAction inputAction)
    {
        string slot1KeyBoard = InputManager.Instance.GetSetting().slot1KeyBoard;
        
        inputAction.AddBinding($"<Keyboard>/{slot1KeyBoard}");
    }
        
    protected override void OnCallback(InputAction.CallbackContext callbackContext)
    {
        bool isClick = callbackContext.ReadValue<float>() > 0;
            
        OnInput?.Invoke(isClick);
    }
}
