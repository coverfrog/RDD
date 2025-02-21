using System;
using System.Collections.Generic;
using Cf.Docs;
using Cf.Pattern;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : Singleton<InputManager>, IInputReceiver
{
    public PlayerInputData Data { get; private set; } = new PlayerInputData();

    private LayerMask _mGroundLayer;

    private int _mRayHitMaxCount;
    private float _mRayHitDistance;
    
    private Ray _mRay;
    private RaycastHit[] _mRayHits;
    
    #region :: Unity

    protected override void Awake()
    {
        // singleton
        base.Awake();
        
        // layer
        _mRayHitMaxCount = 20;
        _mRayHitDistance = 500.0f;
        
        _mGroundLayer = 1 << 11;
        _mRay = new Ray();
        _mRayHits = new RaycastHit[_mRayHitMaxCount];
    }

    #endregion
    
    #region :: Move

    public void OnMoveDir(InputValue inputValue)
    {
        OnMoveDir(inputValue.Get<Vector2>());
    }

    public void OnMoveDir(Vector2 value)
    {
        Data.move.isMoveDirInput = value.sqrMagnitude > 0;
        Data.move.moveDirNormal = new Vector3(value.x, 0, value.y).normalized;
    }
    
    #endregion

    #region :: RightClick

    public void OnRightClick(InputValue inputValue)
    {
        OnRightClick(inputValue.Get<float>());
    }

    public void OnRightClick(float value)
    {
        Data.common.isRightClickInput = Mathf.Approximately(value, 1);
        Data.move.isMovePointInput = false;
            
        if (!Data.common.isRightClickInput)
        {
            return;
        }

        // ray
        _mRay = CamManager.Instance.MainCam.ScreenPointToRay(Input.mousePosition);

        int rayHitCount = Physics.RaycastNonAlloc(_mRay, _mRayHits, _mRayHitDistance, _mGroundLayer);
        if (rayHitCount <= 0)
        {
            return;
        }

        // hit
        RaycastHit nearGroundHit = _mRayHits[0];
        for (int i = 1; i < rayHitCount; i++) 
        {
            if (_mRayHits[i].distance < nearGroundHit.distance)
            {
                nearGroundHit = _mRayHits[i];
            }
        }
        
        // col
        Collider col = nearGroundHit.collider;
        if (!col)
        {
            return;
        }

        // to value
        Data.move.isMovePointInput = true;
        Data.move.movePoint = nearGroundHit.point;
    }

    #endregion

    #region :: Attack

    public void OnAttack(InputValue inputValue)
    {
        OnAttack(inputValue.Get<float>());
    }

    public void OnAttack(float value)
    {
        
    }
    
    #endregion

    #region :: Click

    public void OnClick(InputValue inputValue)
    {
        OnClick(inputValue.Get<float>());
    }

    public void OnClick(float value)
    {
        Data.attack.isAttackMouseInput = Mathf.Approximately(value, 1);
    }
    
    #endregion

}
