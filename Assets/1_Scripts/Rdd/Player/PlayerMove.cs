using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IPlayerMoveToDir
{
    [SerializeField] private bool mIsMoveInput;
    [SerializeField] private Vector3 mMoveDirNormal;

    #region :: Unity

    private void Update()
    {
        if (mIsMoveInput)
        {
            transform.position += mMoveDirNormal * (Time.deltaTime * 3.0f);
        }
    }

    #endregion
    
    #region :: IPlayerMove

    public void OnMove(Vector2 dir, float speed)
    {
        mIsMoveInput = dir.magnitude > 0;
        mMoveDirNormal = new Vector3(dir.x, 0, dir.y).normalized;
    }

    #endregion
}
