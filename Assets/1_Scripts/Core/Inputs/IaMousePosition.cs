using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cf.Inputs
{
    public class IaMousePosition : IaBase
    {
        public event Action<Vector2> OnInput; 
        
        private void Awake()
        {
            // init
            mInputAction = new InputAction("Mouse Position");
            
            // bind
            mInputAction.AddBinding("<Mouse>/position");

            // event
            mInputAction.performed += OnCallback;
            mInputAction.canceled += OnCallback;
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
