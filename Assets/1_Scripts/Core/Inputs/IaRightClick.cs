using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cf.Inputs
{
    public class IaRightClick : IaBase
    {
        public event Action<Vector2> OnMovePoint;
        
        private void Awake()
        {
            // init
            mInputAction = new InputAction("Move Point");
            
            // bind
            mInputAction.AddBinding("<Mouse>/rightButton");
            mInputAction.AddBinding("<Mouse>/position");

            // event
            mInputAction.performed += OnCallback;
            mInputAction.canceled += OnCallback;
        }

        protected override void OnCallback(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.control.IsPressed())
            {
                return;
            }

            Vector2 pos = callbackContext.ReadValue<Vector2>();

            float clampedX = Mathf.Clamp(pos.x, 0, Screen.width);
            float clampedY = Mathf.Clamp(pos.y, 0, Screen.height);

            OnMovePoint?.Invoke(new Vector2(clampedX, clampedY));
        }
    }
}
