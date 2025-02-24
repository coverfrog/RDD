using System;
using UnityEngine;
using UnityEngine.AI;

public class UnitMoveToDirNavMesh : MonoBehaviour, IMove
{
    private NavMeshAgent _mNavMeshAgent;
    
    private void OnEnable()
    {
        if (TryGetComponent<NavMeshAgent>(out _mNavMeshAgent))
        {
            return;
        }

        _mNavMeshAgent = gameObject.AddComponent<NavMeshAgent>();
    }

    private void OnDisable()
    {
        if (_mNavMeshAgent)
        {
            Destroy(_mNavMeshAgent);
        }
    }

    public void OnMove(PlayerInputMoveData data, UnitMoveOption option)
    {
        if (!data.isMoveDirInput)
        {
            return;
        }
        
        if (!_mNavMeshAgent)
        {
            return;
        }

        if (!_mNavMeshAgent.isOnNavMesh)
        {
            return;
        }

        _mNavMeshAgent.Move(data.moveDirNormal * (Time.deltaTime * option.moveSpeed));
    }
}
