using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cf.Inputs
{
    public class IaMovePoint : IaBase
    {
        public event Action<RaycastHit> OnMovePoint;
        
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
            Debug.Log(callbackContext.ReadValue<float>());
        }
    }
}
