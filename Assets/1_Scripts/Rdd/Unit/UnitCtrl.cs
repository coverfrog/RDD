using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public abstract class UnitCtrl : MonoBehaviour
{
    [Header("Type")] 
    [SerializeField] private UnitMoveType mMoveFuncType;
    [SerializeField] private UnitAttackType mAttackFuncType;
    
    [Header("Option")] 
    [SerializeField] private UnitMoveOption mMoveOption;
    [SerializeField] private UnitAttackOption mAttackOption;

    private IMove _mIMove;
    private IAttack _mIAttack;

    #region :: Unit Type

    public abstract UnitType GetUnitType { get; }

    #endregion
    
    #region :: Unity

    protected virtual void Awake()
    {
        OnInitMoveFuncType();
        OnInitAttackFuncType();
    }

    protected virtual void Update()
    {
        _mIMove?.OnMove(InputManager.Instance.Data.move, mMoveOption);
        _mIAttack?.OnAttack(InputManager.Instance.Data.attack, mAttackOption);
    }

    #endregion
    
    #region :: On Change Func Type

    private void OnInitMoveFuncType()
    {
        if (TryGetComponent(out UnitMoveToDir toDir)) 
            Destroy(toDir);
        if (TryGetComponent(out UnitMoveToDirNavMesh toDirNavMesh)) 
            Destroy(toDirNavMesh);
        if (TryGetComponent(out UnitMoveToPoint toPoint)) 
            Destroy(toPoint);
        if (TryGetComponent(out UnitMoveToPointNavMesh toPointNavMesh)) 
            Destroy(toPointNavMesh);
        
        
        _mIMove = mMoveFuncType switch
        {
            UnitMoveType.ToNone =>
                null,
            UnitMoveType.ToDir => 
                gameObject.AddComponent<UnitMoveToDir>(),
            UnitMoveType.ToDirNavMesh =>
                gameObject.AddComponent<UnitMoveToDirNavMesh>(),
            UnitMoveType.ToPoint =>
                gameObject.AddComponent<UnitMoveToPoint>(),
            UnitMoveType.ToPointNavMesh =>
                gameObject.AddComponent<UnitMoveToPointNavMesh>(),
            _ => 
                throw new ArgumentOutOfRangeException()
        };
    }

    private void OnInitAttackFuncType()
    {
        if (TryGetComponent(out UnitAttackToBodySlam toBodySlam)) 
            Destroy(toBodySlam);
        if (TryGetComponent(out UnitAttackToFist toFist)) 
            Destroy(toFist);
        if (TryGetComponent(out UnitAttackToShortSward toShortSward)) 
            Destroy(toShortSward);
        
        _mIAttack = mAttackFuncType switch
        {
            UnitAttackType.ToNone =>
                null,
            UnitAttackType.ToBodySlam => 
                gameObject.AddComponent<UnitAttackToBodySlam>(),
            UnitAttackType.ToFist => 
                gameObject.AddComponent<UnitAttackToFist>(),
            UnitAttackType.ToShortSward => 
                gameObject.AddComponent<UnitAttackToShortSward>(),
            _ => 
                throw new ArgumentOutOfRangeException()
        };
    }

    #endregion
}
