using System;
using UnityEngine;

public class PlayerMoveToDir : MonoBehaviour, IMove
{
    public void OnMove(PlayerInputMoveData data, float speed)
    {
        if (!data.isMoveDirInput)
        {
            return;
        }

        transform.position += data.moveDirNormal * (Time.deltaTime * speed);
    }
}
