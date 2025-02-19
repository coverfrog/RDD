using UnityEngine;
using UnityEngine.InputSystem;

#region :: Player Input

public interface IPlayerInputValue
{
    public void OnMove(InputValue inputValue);
}

public interface IPlayerInputAct 
{
    public void OnMove(Vector2 value);
}

public interface IPlayerInputSender : IPlayerInputValue, IPlayerInputAct
{
    public void OnPlayerInputSub(IPlayerInputReceiver receiver);
    public void OnPlayerInputUnSub(IPlayerInputReceiver receiver);
}

public interface IPlayerInputReceiver : IPlayerInputAct
{
    public void PlayerInputSub(IPlayerInputSender sender);
    public void PlayerInputUnSub(IPlayerInputSender sender);
}

#endregion

#region :: Command

public interface IInputCommandSender
{
    public void SendCommand(InputCommandName commandName);

    public void OnInputCommandSub(IInputCommandReceiver receiver);

    public void OnInputCommandUnSub(IInputCommandReceiver receiver);
}

public interface IInputCommandReceiver
{
    public void OnSendCommand(InputCommandName commandName);

    public void InputCommandSub(IInputCommandSender sender);

    public void InputCommandUnSub(IInputCommandSender sender);
    
}

#endregion
