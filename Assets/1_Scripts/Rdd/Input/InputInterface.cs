using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

#region :: Data

[System.Serializable]
public class PlayerInputData
{
    public PlayerInputCommonData common = new PlayerInputCommonData();
    public PlayerInputMoveData move = new PlayerInputMoveData();
    public PlayerInputAttackData attack = new PlayerInputAttackData();
}

[System.Serializable]
public class PlayerInputCommonData
{
    public bool isRightClickInput;
}

[System.Serializable]
public class PlayerInputMoveData
{
    public bool isMoveDirInput;
    public bool isMovePointInput;

    public Vector3 moveDirNormal;
    public Vector3 movePoint;
}

[System.Serializable]
public class PlayerInputAttackData
{
    public bool isAttackMouseInput;
}

#endregion

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

public enum MoveFuncType
{
    ToNone,
    ToDir,
    ToPoint,
}

public interface IMove 
{
    public void OnMove(PlayerInputMoveData data, float speed);
}

public interface IMoveDirInput 
{
    public void OnMoveDir(InputValue inputValue);
    
    public void OnMoveDir(Vector2 value);
}

#endregion

#region :: Attack

public enum AttackFuncType
{
    ToNone,
    ToBodySlam,
    ToFist,
    ToShortSward,
}

public interface IAttack
{
    public void OnAttack(PlayerInputAttackData data);
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