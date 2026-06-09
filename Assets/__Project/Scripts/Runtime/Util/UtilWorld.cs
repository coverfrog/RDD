
using UnityEngine;
using UnityEngine.InputSystem;

public static class UtilWorld
{
    public static bool TryGetMouseGroundPoint(out Vector3 groundPoint)
    {
        return TryGetMouseGroundPoint(Mouse.current.position.value, out groundPoint);
    }

    public static bool TryGetMouseGroundPoint(Vector2 screenPosition, out Vector3 groundPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Ground")))
        {
            groundPoint = hit.point;
            return true;
        }
        groundPoint = Vector3.zero;
        return false;
    }
}
