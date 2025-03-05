using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;

namespace Cf.Inputs
{
    [Serializable]
    public class IaEventCondition
    {
        public bool started = false;
        public bool canceled = true;
        public bool performed = true;
    }

    public abstract class IaBase<T> : MonoBehaviour where T : struct
    {
        private IaEventCondition _mIaEventCondition;
        private InputAction _mInputAction;

        public abstract event Action<T> OnInput;

        protected abstract void SetEventCondition(ref IaEventCondition condition);
        
        protected abstract void AddBinding(ref InputAction inputAction);

        protected abstract void OnCallback(InputAction.CallbackContext callbackContext);
        
        public void Begin()
        {
            _mIaEventCondition = new IaEventCondition();
            _mInputAction = new InputAction();

            SetEventCondition(ref _mIaEventCondition);
            AddBinding(ref _mInputAction);

            if (_mIaEventCondition.started)   
                _mInputAction.started += OnCallback;
            if (_mIaEventCondition.performed) 
                _mInputAction.performed += OnCallback;
            if (_mIaEventCondition.canceled)  
                _mInputAction.canceled += OnCallback;
            
            _mInputAction?.Enable();
        }

        private void OnDisable()
        {
            _mInputAction?.Disable();
        }
    }
}
