using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public class ProjectileCtrl : NetworkBehaviour
{
    [SyncVar]
    private float m_speed;

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

    public void Setup(float speed)
    {
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
}
