using System;
using UnityEngine;

public class UnitMoveDirection : MonoBehaviour
{
    [SerializeField] private UnitMovementData data;
    
    private void Update()
    {
        OnMove(InputManager.Instance.Data.directionVector3, data.moveSpeed);
    }

    private void OnMove(Vector3 dir, float speed)
    {
        transform.position += dir * (Time.deltaTime * speed);
    }
}
