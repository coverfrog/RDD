using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cf.Inputs;

public class IaSlot0 : IaBase<bool>
{
    public override event Action<bool> OnInput;

    protected override void SetEventCondition(ref IaEventCondition condition)
    {
            
    }

    protected override void AddBinding(ref InputAction inputAction)
    {
        string slot0KeyBoard = InputManager.Instance.GetSetting().slot0KeyBoard;
        
        inputAction.AddBinding($"<Keyboard>/{slot0KeyBoard}");
    }
        
    protected override void OnCallback(InputAction.CallbackContext callbackContext)
    {
        bool isClick = callbackContext.ReadValue<float>() > 0;
            
        OnInput?.Invoke(isClick);
    }
}