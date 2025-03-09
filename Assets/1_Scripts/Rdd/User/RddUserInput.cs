using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class RddUserInput : MonoBehaviour
{
    public event Action<RddUserInputData> OnDataChange; 
    private RddUserInputData Data {
        get => _mData;
        set
        {
            _mData = value;
            
            OnDataChange?.Invoke(_mData);
        }
    }

    private RddUserInputData _mData = new RddUserInputData();
    
    private PlayerInput _mPlayerInput;
        
    private void Awake()
    {
        _mPlayerInput = gameObject.AddComponent<PlayerInput>();
        _mPlayerInput.actions = Resources.Load<InputActionAsset>("Input/InputSystem_Actions");
    }
    
    private void Start()
    {
        OnDataChange?.Invoke(_mData);
    }

    public void OnMoveDir(InputValue inputValue)
    {
        var value = inputValue.Get<Vector2>();

        Data.moveDirection = new Vector3(value.x, 0, value.y).normalized;
    }
}
