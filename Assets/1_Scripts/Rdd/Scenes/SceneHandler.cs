using System;
using UnityEngine;

public abstract class SceneHandler : MonoBehaviour, IInputCommandReceiver
{
    #region :: Unity

    private void OnEnable()
    {
        InputCommandSub(InputManager.Instance);
    }

    private void OnDisable()
    {
        InputCommandUnSub(InputManager.Instance);
    }

    #endregion

    #region :: IInputCommand

    public void InputCommandSub(IInputCommandSender sender)
    {
        sender?.OnInputCommandSub(this);
    }

    public void InputCommandUnSub(IInputCommandSender sender)
    {
        sender?.OnInputCommandUnSub(this);
    }

    public void OnSendCommand(InputCommandName commandName)
    {
        
    }

    #endregion
}
