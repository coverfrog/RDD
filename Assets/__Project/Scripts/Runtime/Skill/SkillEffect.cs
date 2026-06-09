using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillEffect", menuName = "RDD/SkillEffect")]
public class SkillEffect : ScriptableObject
{
    [Tooltip("투사체(발사체) 형태의 이펙트를 생성하여 날려보낼지 여부입니다.")]
    [SerializeField] private bool m_isProjectile;

    [Tooltip("날아갈 투사체 오브젝트의 프리팹을 지정합니다.")]
    [SerializeField] private ProjectileCtrl m_projectilePrefab;

    [Tooltip("투사체가 날아가는 속도를 결정합니다.")]
    [SerializeField] private float m_projectileSpeed = 10f;

    [Tooltip("스킬 시전자 주변을 지속적으로 감싸는 연출(아우라, 보호막 등)을 사용할지 여부입니다.")]
    [SerializeField] private bool m_hasSurroundingEffect;

    [Tooltip("시전자 주변에 생성 및 부착하여 유지할 이펙트 프리팹을 지정합니다.")]
    [SerializeField] private GameObject m_surroundingEffectPrefab;

    [Tooltip("타격 지점이나 대상 객체의 위치에서 즉시 폭발하는 연출(피격/폭발 이펙트)을 사용할지 여부입니다.")]
    [SerializeField] private bool m_hasHitEffect;

    [Tooltip("피격 또는 타격 위치에서 순간적으로 생성되었다가 사라질 폭발 이펙트 프리팹을 지정합니다.")]
    [SerializeField] private GameObject m_hitEffectPrefab;

    public bool IsProjectile => m_isProjectile;
    public ProjectileCtrl ProjectilePrefab => m_projectilePrefab;
    public float ProjectileSpeed => m_projectileSpeed;

    public bool HasSurroundingEffect => m_hasSurroundingEffect;
    public GameObject SurroundingEffectPrefab => m_surroundingEffectPrefab;

    public bool HasHitEffect => m_hasHitEffect;
    public GameObject HitEffectPrefab => m_hitEffectPrefab;
}
