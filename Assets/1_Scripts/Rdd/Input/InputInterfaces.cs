
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerInput
{
    public void OnMove(InputValue inputValue);

    public void OnMove(Vector2 vector2);
}

public interface IInputCommandSender
{
    public void OnSub(IInputCommandReceiver receiver);

    public void OnUnSub(IInputCommandReceiver receiver);
    
    public void SendCommand(InputCommandName commandName);
}

public interface IInputCommandReceiver
{
    public void Sub(IInputCommandSender sender);

    public void UnSub(IInputCommandSender sender);
    
    public void OnSendCommand(InputCommandName commandName);
}