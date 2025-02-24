using UnityEngine;
using UnityEngine.InputSystem;

#region :: Receiver

public interface IInputReceiver : 
    IMoveDirInput,
    IRightClickInput,
    IAttackInput,
    IClickInput
{
    
}

#endregion

#region :: Move

public interface IMove 
{
    public void OnMove(PlayerInputMoveData data, UnitMoveOption option);
}

public interface IMoveDirInput 
{
    public void OnMoveDir(InputValue inputValue);
    
    public void OnMoveDir(Vector2 value);
}

#endregion

#region :: Attack

public interface IAttack
{
    public void OnAttack(PlayerInputAttackData data, UnitAttackOption option);
}

public interface IAttackInput
{
    public void OnAttack(InputValue inputValue);

    public void OnAttack(float value);
}

#endregion

#region :: Click

public interface IClick
{
    public void OnClick(bool value);
}

public interface IClickInput
{
    public void OnClick(InputValue inputValue);

    public void OnClick(float value);
}

#endregion

#region :: RightClick

public interface IRightClick
{
    public void OnRightClick(bool value);
}

public interface IRightClickInput
{
    public void OnRightClick(InputValue inputValue);

    public void OnRightClick(float value);
}

#endregion