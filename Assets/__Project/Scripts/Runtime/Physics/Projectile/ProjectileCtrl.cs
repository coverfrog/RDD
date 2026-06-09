using Mirror;
using System;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public class ProjectileCtrl : NetworkBehaviour
{
    [SyncVar]
    private float m_speed;

    private PlayerCtrl m_owner;

    public event Action<PlayerCtrl> OnHitPlayer;

    #region : Rigidbody

    public Rigidbody Rb3d
    {
        get
        {
            if (m_rb3d == null) m_rb3d = GetComponent<Rigidbody>();
            return m_rb3d;
        }
    }

    internal Rigidbody m_rb3d;

    #endregion

    public void Setup(PlayerCtrl owner, float speed)
    {
        m_owner = owner;
        m_speed = speed;
    }

    public override void OnStartServer()
    {
        // 5초 후 서버에서 객체 파괴 (Mirror를 통해 모든 클라이언트에 동기화됨)
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        if (Rb3d.isKinematic == true)
            return;

        Rb3d.linearVelocity = transform.forward* m_speed;
    }

    private void OnCol(GameObject obj)
    {
        if (isServer == false) return;

        PlayerCtrl hitPlayer = obj.GetComponentInParent<PlayerCtrl>();
        if (hitPlayer != null)
        {
            if (hitPlayer == m_owner)
                return;

            OnHitPlayer?.Invoke(hitPlayer);

            NetworkServer.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCol(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCol(collision.gameObject);
    }
}
