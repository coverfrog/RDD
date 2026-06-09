using Mirror;
using UnityEngine;

public class StatCtrl : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private float m_moveSpeed = 3.0f;

    [SyncVar]
    [SerializeField] private float m_rotateSpeed = 10.0f;

    public float MoveSpeed => m_moveSpeed;

    public float RotateSpeed => m_rotateSpeed;
}
