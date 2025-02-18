using System;
using UnityEngine;

public abstract class SceneHandler : MonoBehaviour, IInputCommandReceiver
{
    private void OnEnable()
    {
        Sub(InputManager.Instance);
    }

    private void OnDisable()
    {
        UnSub(InputManager.Instance);
    }

    #region :: IInputCommand

    public void Sub(IInputCommandSender sender)
    {
        sender.OnSub(this);
    }

    public void UnSub(IInputCommandSender sender)
    {
        sender.OnUnSub(this);
    }

    public abstract void OnSendCommand(InputCommandName commandName);

    #endregion

}
