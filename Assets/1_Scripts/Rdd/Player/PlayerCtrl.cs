using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour, IPlayerInputReceiver
{
    [Header("Option")] 
    [SerializeField] [Min(0.0f)] private float mMoveSpeed = 3.0f;
    
    [Header("Reference")]
    [SerializeField] private PlayerMove mPlayerMove;
    
    #region :: Unity

    private void OnEnable()
    {
        PlayerInputSub(InputManager.Instance);
    }

    private void OnDisable()
    {
        PlayerInputUnSub(InputManager.Instance);
    }

    #endregion

    #region :: PlayerInput

    public void OnMove(Vector2 value)
    {
        mPlayerMove?.OnMove(value, mMoveSpeed);
    }

    public void PlayerInputSub(IPlayerInputSender sender)
    {
        sender?.OnPlayerInputSub(this);
    }

    public void PlayerInputUnSub(IPlayerInputSender sender)
    {
        sender?.OnPlayerInputUnSub(this);
    }
    
    #endregion
}
