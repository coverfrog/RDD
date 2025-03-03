using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cf.Inputs
{
    public class InputActionDirection : InputActionBase
    {
        public event Action<Vector3> OnMove;

        private void Awake()
        {
            // init
            mInputAction = new InputAction("Move", InputActionType.Value);

            // bind
            mInputAction.AddBinding("<Gamepad>/leftStick");

            mInputAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            
            // event
            mInputAction.performed += OnCallback;
            mInputAction.canceled += OnCallback;
        }

        protected override void OnCallback(InputAction.CallbackContext callbackContext)
        {
            Vector2 value = callbackContext.ReadValue<Vector2>().normalized;

            OnMove?.Invoke(new Vector3(value.x, 0 ,value.y));
        }
    }
}
