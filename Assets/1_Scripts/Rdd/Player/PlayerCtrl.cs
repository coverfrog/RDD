using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerCtrl : MonoBehaviour
{
    [Header("Option")] 
    [SerializeField] private MoveFuncType mMoveFuncType;
    [SerializeField] private AttackFuncType mAttackFuncType;
    
    [Header("Status")] 
    [SerializeField] [Min(0.0f)] private float mMoveSpeed = 3.0f;

    private IMove _mIMove;
    private IAttack _mIAttack;
    
    #region :: Unity

    private void Awake()
    {
        OnInitMoveFuncType();
        OnInitAttackFuncType();
    }

    private void Update()
    {
        _mIMove?.OnMove(InputManager.Instance.Data.move, mMoveSpeed);
        _mIAttack?.OnAttack(InputManager.Instance.Data.attack);
    }

    #endregion
    
    #region :: On Change Func Type

    private void OnInitMoveFuncType()
    {
        if (TryGetComponent(out PlayerMoveToDir toDir)) Destroy(toDir);
        if (TryGetComponent(out PlayerMoveToPoint toPoint)) Destroy(toPoint);
        
        _mIMove = mMoveFuncType switch
        {
            MoveFuncType.ToNone =>
                null,
            MoveFuncType.ToDir => 
                gameObject.AddComponent<PlayerMoveToDir>(),
            MoveFuncType.ToPoint =>
                gameObject.AddComponent<PlayerMoveToPoint>(),
            _ => 
                throw new ArgumentOutOfRangeException()
        };
    }

    private void OnInitAttackFuncType()
    {
        _mIAttack = mAttackFuncType switch
        {
            AttackFuncType.ToNone =>
                null,
            AttackFuncType.ToBodySlam => 
                gameObject.AddComponent<PlayerAttackToBodySlam>(),
            AttackFuncType.ToFist => 
                gameObject.AddComponent<PlayerAttackToFist>(),
            AttackFuncType.ToShortSward => 
                gameObject.AddComponent<PlayerAttackToShortSward>(),
            _ => 
                throw new ArgumentOutOfRangeException()
        };
    }

    #endregion
}
