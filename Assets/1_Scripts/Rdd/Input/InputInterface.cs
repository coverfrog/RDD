using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

#region :: Data

[System.Serializable]
public class PlayerInputData
{
    public PlayerInputCommonData common = new PlayerInputCommonData();
    public PlayerInputRayData ray = new PlayerInputRayData();
    public PlayerInputMoveData move = new PlayerInputMoveData();
}

[System.Serializable]
public class PlayerInputCommonData
{
    public bool isRightClickInput;
}

[System.Serializable]
public class PlayerInputRayData
{
    public RaycastHit currentGroundHit;
}

[System.Serializable]
public class PlayerInputMoveData
{
    public bool isMoveDirInput;
    public bool isMovePointInput;

    public Vector3 moveDirNormal;
    public Vector3 movePoint;
}

#endregion

#region :: Receiver

public interface IInputReceiver : 
    IMoveDirInput,
    IRightClickInput
{
    
}


#endregion

#region :: Move

public enum MoveFuncType
{
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