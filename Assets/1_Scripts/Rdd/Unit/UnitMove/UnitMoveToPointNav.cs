using System;
using Cf.Cams;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMoveToPointNav : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private NavMeshAgent mNavMeshAgent;

    private bool _mIsRightClick;
    private Vector2 _mMousePosition;
    
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

    private void OnRightClick(bool b)
    {
        _mIsRightClick = b;
    }
    
    private void OnMousePosition(Vector2 vector2)
    {
        _mMousePosition = vector2;
    }

    private void Update()
    {
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
}
