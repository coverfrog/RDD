using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerCtrl : MonoBehaviour
{
    [Header("Option")] 
    [SerializeField] private MoveFuncType mMoveFuncType;
    
    [Header("Status")] 
    [SerializeField] [Min(0.0f)] private float mMoveSpeed = 3.0f;

    private IMove _mIMove;
    
    #region :: Unity

    private void Awake()
    {
        OnInitMoveFuncType();
    }

    private void Update()
    {
        _mIMove?.OnMove(InputManager.Instance.Data.move, mMoveSpeed);
    }

    #endregion
    
    #region :: On Change Func Type

    private void OnInitMoveFuncType()
    {
        if (TryGetComponent(out PlayerMoveToDir toDir)) Destroy(toDir);
        if (TryGetComponent(out PlayerMoveToPoint toPoint)) Destroy(toPoint);
        
        _mIMove = mMoveFuncType switch
        {
            MoveFuncType.ToDir => 
                gameObject.AddComponent<PlayerMoveToDir>(),
            MoveFuncType.ToPoint =>
                gameObject.AddComponent<PlayerMoveToPoint>(),
            _ => 
                throw new ArgumentOutOfRangeException()
        };
    }

    #endregion
}
