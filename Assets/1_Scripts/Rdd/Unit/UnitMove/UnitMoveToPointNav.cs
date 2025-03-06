using System;
using Cf.Cams;
using Cf.Utils;
using UnityEngine;
using UnityEngine.AI;

public class UnitMoveToPointNav : MonoBehaviour
{
    [Header("Option")] 
    [SerializeField] private bool mIsAvailableAwake = true;
    
    [Header("Reference")]
    [SerializeField] private NavMeshAgent mNavMeshAgent;

    private bool _mIsAvailable;
    
    private bool _mIsRightClick;
    private Vector2 _mMousePosition;

    #region :: Set

    public void SetIsAvailable(bool available)
    {
        _mIsAvailable = available;
    }

    #endregion
    
    #region :: Unity

    private void Awake()
    {
        CfUtil.Components.TryAddComponent(this, out mNavMeshAgent);

        _mIsAvailable = mIsAvailableAwake;
    }

    private void OnEnable()
    {
        InputManager.Instance.OnRightClick += OnRightClick;
        InputManager.Instance.OnMousePosition += OnMousePosition;
    }

    private void OnDisable()
    {
        if (!InputManager.Instance)
        {
            return;
        }

        InputManager.Instance.OnRightClick -= OnRightClick;
        InputManager.Instance.OnMousePosition -= OnMousePosition;
    }
    
    private void Update()
    {
        if (!_mIsAvailable)
        {
            return;
        }

        if (!_mIsRightClick)
        {
            return;
        }

        if (!CamUtil.ToRay(_mMousePosition, 1 << 11, out var hitCount, out var result))
        {
            return;
        }
        
        Vector3 nearHitPos = CamUtil.GetNearHit(hitCount, result).point;

        mNavMeshAgent.destination = new Vector3(nearHitPos.x, 0, nearHitPos.z);
    }

    #endregion

    #region :: Input Event

    private void OnRightClick(bool b)
    {
        _mIsRightClick = b;
    }
    
    private void OnMousePosition(Vector2 vector2)
    {
        _mMousePosition = vector2;
    }

    #endregion
}
