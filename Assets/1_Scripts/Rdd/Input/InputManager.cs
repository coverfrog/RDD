using System;
using System.Collections.Generic;
using Cf.Docs;
using Cf.Pattern;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : GenericSingleton<InputManager>, IInputCommandSender, IPlayerInput
{
    private readonly List<IInputCommandReceiver> _mCommandReceiverList = new List<IInputCommandReceiver>();

    private readonly Queue<Vector2> _mOnMoveQueue = new Queue<Vector2>();
    
    #region :: Player Input

    public void OnMove(InputValue inputValue)
    {
        OnMove(inputValue.Get<Vector2>());
    }

    public void OnMove(Vector2 value)
    {
        Debug.Log(value);
        
        _mOnMoveQueue.Enqueue(value);
    }

    #endregion

    private void Update()
    {
        if (_mOnMoveQueue.Count > 0)
        {
            
        }
    }

    #region :: IInputCommand

    public void OnSub(IInputCommandReceiver receiver)
    {
        _mCommandReceiverList.Add(receiver);
    }

    public void OnUnSub(IInputCommandReceiver receiver)
    {
        _mCommandReceiverList.Remove(receiver);
    }

    public void SendCommand(InputCommandName commandName)
    {
        foreach (IInputCommandReceiver receiver in _mCommandReceiverList)
        {
            receiver.OnSendCommand(commandName);
        }
    }

    #endregion
}
