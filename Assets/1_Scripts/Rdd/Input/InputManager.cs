using System;
using System.Collections.Generic;
using Cf.Docs;
using Cf.Pattern;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : Singleton<InputManager>, IInputCommandSender, IPlayerInputSender
{
    private readonly List<IInputCommandReceiver> _mCommandReceiverList = new List<IInputCommandReceiver>();
    private readonly List<IPlayerInputReceiver> _mOPlayerInputReceiverList = new List<IPlayerInputReceiver>();
    
    #region :: Player Input

    public void OnPlayerInputSub(IPlayerInputReceiver receiver)
    {
        _mOPlayerInputReceiverList.Add(receiver);
    }

    public void OnPlayerInputUnSub(IPlayerInputReceiver receiver)
    {
        _mOPlayerInputReceiverList.Remove(receiver);
    }
    
    public void OnMove(InputValue inputValue)
    {
        OnMove(inputValue.Get<Vector2>());
    }

    public void OnMove(Vector2 value)
    {
        foreach (IPlayerInputReceiver receiver in _mOPlayerInputReceiverList)
        {
            receiver.OnMove(value);
        }
    }

    #endregion

    #region :: IInputCommand

    public void OnInputCommandSub(IInputCommandReceiver receiver)
    {
        _mCommandReceiverList.Add(receiver);
    }

    public void OnInputCommandUnSub(IInputCommandReceiver receiver)
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
