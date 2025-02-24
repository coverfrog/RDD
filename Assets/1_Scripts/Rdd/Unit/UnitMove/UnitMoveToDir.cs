using System;
using UnityEngine;

public class UnitMoveToDir : MonoBehaviour, IMove
{
    public void OnMove(PlayerInputMoveData data, UnitMoveOption option)
    {
        if (!data.isMoveDirInput)
        {
            return;
        }

        transform.position += data.moveDirNormal * (Time.deltaTime * option.moveSpeed);
    }
}
