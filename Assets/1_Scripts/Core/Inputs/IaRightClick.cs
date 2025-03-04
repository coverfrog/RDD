using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cf.Inputs
{
    public class IaRightClick : IaBase
    {
        public event Action<bool> OnInput; 
        
        private void Awake()
        {
            // init
            mInputAction = new InputAction("Right Click");
            
            // bind
            mInputAction.AddBinding("<Mouse>/rightButton");

            // event
            mInputAction.performed += OnCallback;
            mInputAction.canceled += OnCallback;
        }

        protected override void OnCallback(InputAction.CallbackContext callbackContext)
        {
            bool isClick = callbackContext.ReadValue<float>() > 0;
            
            OnInput?.Invoke(isClick);
        }
    }
}
