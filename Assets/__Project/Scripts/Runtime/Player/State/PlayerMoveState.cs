using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public override void FixedUpdate()
    {
        if (Owner.Rb3d.isKinematic == true)
            return;

        Vector3 direction = GetDirection();

        if (direction.sqrMagnitude <= 0.001f)
            return;

        direction.Normalize();

        SetVelocity(direction);
        SetRotation(direction);
    }

    public override void Exit()
    {
        if (Owner.Rb3d.isKinematic == true)
            return;

        ClearVelocity();
    }

    private void ClearVelocity()
    {
        Vector3 vel = Owner.Rb3d.linearVelocity;
        Owner.Rb3d.linearVelocity = new Vector3(0, vel.y, 0);
    }

    private Vector3 GetDirection()
    {
        Vector3 target = Owner.CurrentInputContext.MoveGroundPoint;
        Vector3 current = Owner.transform.position;
        Vector3 direction = target - current;
        direction.y = 0;

        return direction;
    }

    private void SetVelocity(Vector3 direction)
    {
        Vector3 currentVel = Owner.Rb3d.linearVelocity;
        Owner.Rb3d.linearVelocity = new Vector3(direction.x * Owner.moveSpeed, currentVel.y, direction.z * Owner.moveSpeed);
    }

    private void SetRotation(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Owner.transform.rotation = Quaternion.Slerp(
            Owner.transform.rotation,
            targetRotation,
            Time.fixedDeltaTime * Owner.rotateSpeed
        );
    }
}

