using UnityEngine;
using UnityEngine.AI;

public class UnitMoveToPointNavMesh : MonoBehaviour, IMove
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
        if (!data.isMovePointInput)
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
     
        _mNavMeshAgent.SetDestination(data.movePoint);
        _mNavMeshAgent.acceleration = option.moveSpeed;
    }
}
