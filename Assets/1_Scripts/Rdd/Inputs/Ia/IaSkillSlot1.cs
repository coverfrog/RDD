using System;
using UnityEngine;
using Cf.Inputs;
using UnityEngine.InputSystem;

public class IaSkillSlot1 : IaSlot
{
    public override event Action<bool> OnInput;

    protected override void SetEventCondition(ref IaEventCondition condition)
    {
            
    }

    public override void UpdateBindKey(IaSetting settings)
    {
        SlotBindKeyboard = settings.slot1KeyBoard;
    }
    
    protected override void AddBinding(ref InputAction inputAction)
    {
        inputAction.AddBinding($"<Keyboard>/{SlotBindKeyboard}");
    }
        
    protected override void OnCallback(InputAction.CallbackContext callbackContext)
    {
        bool isClick = callbackContext.ReadValue<float>() > 0;
            
        OnInput?.Invoke(isClick);
    }
}
