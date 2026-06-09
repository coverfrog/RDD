using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public class ProjectileCtrl : NetworkBehaviour
{
    [SyncVar]
    private float m_speed;

    public void Setup(float speed)
    {
        m_speed = speed;
    }

    public override void OnStartServer()
    {
        // 5초 후 서버에서 객체 파괴 (Mirror를 통해 모든 클라이언트에 동기화됨)
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (m_speed * Time.deltaTime));
    }
}
