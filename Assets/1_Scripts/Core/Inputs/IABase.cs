using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

namespace Cf.Inputs
{
    public abstract class IaBase : MonoBehaviour
    {
        [SerializeField] protected InputAction mInputAction;

        protected abstract void OnCallback(InputAction.CallbackContext callbackContext);
        
        private void OnEnable()
        {
            mInputAction?.Enable();
        }

        private void OnDisable()
        {
            mInputAction?.Disable();
        }
    }
}
