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

    private void OnTriggerEnter(Collider other)
    {
        // 충돌/데미지 판정은 서버에서만 신뢰성 있게 처리
        if (!isServer) return;

        PlayerCtrl hitPlayer = other.GetComponentInParent<PlayerCtrl>();
        if (hitPlayer != null)
        {
            // 자신(투사체 시전자)과의 충돌은 제외
            if (hitPlayer == m_owner) return;

            OnHitPlayer?.Invoke(hitPlayer);

            // 다른 플레이어 피격 시 투사체 파괴
            NetworkServer.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isServer) return;

        PlayerCtrl hitPlayer = collision.gameObject.GetComponentInParent<PlayerCtrl>();
        if (hitPlayer != null)
        {
            if (hitPlayer == m_owner) return;

            OnHitPlayer?.Invoke(hitPlayer);

            // 다른 플레이어 피격 시 투사체 파괴
            NetworkServer.Destroy(gameObject);
        }
    }
}
