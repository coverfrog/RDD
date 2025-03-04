using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cf.Inputs
{
    public class IaRightClick : IaBase
    {
        public event Action<bool> OnClick; 
        public event Action<Vector2> OnValue;
        
        private void Awake()
        {
            // init
            mInputAction = new InputAction("Move Point");
            
            // bind
            mInputAction.AddBinding("<Mouse>/rightButton");

            // event
            mInputAction.performed += OnCallback;
            mInputAction.canceled += OnCallback;
        }

        protected override void OnCallback(InputAction.CallbackContext callbackContext)
        {
            bool isClick = callbackContext.ReadValue<float>() > 0;
            
            OnClick?.Invoke(isClick);
            
            if (!isClick)
            {
                return;
            }

            Vector2 pos = Input.mousePosition;

            float clampedX = Mathf.Clamp(pos.x, 0, Screen.width);
            float clampedY = Mathf.Clamp(pos.y, 0, Screen.height);

            OnValue?.Invoke(new Vector2(clampedX, clampedY));
        }
    }
}
