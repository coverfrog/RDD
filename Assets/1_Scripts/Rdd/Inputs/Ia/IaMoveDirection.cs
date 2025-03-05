using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cf.Inputs;

public class IaMoveDirection : IaBase<Vector3>
{
    public override event Action<Vector3> OnInput;

    protected override void SetEventCondition(ref IaEventCondition condition)
    {
            
    }

    protected override void AddBinding(ref InputAction inputAction)
    {
        inputAction.AddBinding("<Gamepad>/leftStick");

        inputAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
    }

    protected override void OnCallback(InputAction.CallbackContext callbackContext)
    {
        Vector2 value = callbackContext.ReadValue<Vector2>().normalized;

        OnInput?.Invoke(new Vector3(value.x, 0 ,value.y));
    }
}