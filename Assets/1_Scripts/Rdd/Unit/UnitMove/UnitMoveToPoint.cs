using UnityEngine;

public class UnitMoveToPoint : MonoBehaviour, IMove
{
    private const float EndDistance = 0.1f;

    private bool _mIsMoving;
    private Vector3 _mMovePoint;
    private float _mSpeed;

    private void Update()
    {
        if (!_mIsMoving)
        {
            return;
        }
        
        float distance = Vector3.Distance(transform.position, _mMovePoint);

        if (distance < EndDistance)
        {
            _mIsMoving = false;
            _mMovePoint = transform.position;

            return;
        }

        Vector3 dir = (_mMovePoint - transform.position).normalized;
        dir.y = 0;

        transform.position += dir * (Time.deltaTime * _mSpeed);
    }
    
    public void OnMove(PlayerInputMoveData data, UnitMoveOption option)
    {
        if (!data.isMovePointInput)
        {
            return;
        }

        _mIsMoving = true;
        _mMovePoint = data.movePoint;
        _mSpeed = option.moveSpeed;
    }
}
