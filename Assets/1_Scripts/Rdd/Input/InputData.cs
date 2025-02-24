using UnityEngine;

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