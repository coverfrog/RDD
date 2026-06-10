using System;
using UnityEngine;

[Serializable]
public abstract class SkillEffect
{
    public virtual bool IsProjectile => false;
    public virtual ProjectileCtrl ProjectilePrefab => null;

    public virtual bool IsDash => false;

    public virtual bool IsSurroundingEffect => false;
    public virtual GameObject SurroundingEffectPrefab => null;

    public virtual bool IsHitEffect => false;
    public virtual GameObject HitEffectPrefab => null;
}

[Serializable]
[AddTypeMenu("Projectile")]
public class ProjectileSkillEffect : SkillEffect
{
    [Tooltip("날아갈 투사체 오브젝트의 프리팹을 지정합니다.")]
    [SerializeField] private ProjectileCtrl m_projectilePrefab;

    public override bool IsProjectile => true;
    public override ProjectileCtrl ProjectilePrefab => m_projectilePrefab;
}

[Serializable]
[AddTypeMenu("Dash")]
public class DashSkillEffect : SkillEffect
{
    public override bool IsDash => true;
}

[Serializable]
[AddTypeMenu("Surrounding")]
public class SurroundingSkillEffect : SkillEffect
{
    [Tooltip("시전자 주변에 생성 및 부착하여 유지할 이펙트 프리팹을 지정합니다.")]
    [SerializeField] private GameObject m_surroundingEffectPrefab;

    public override bool IsSurroundingEffect => true;
    public override GameObject SurroundingEffectPrefab => m_surroundingEffectPrefab;
}

[Serializable]
[AddTypeMenu("Hit")]
public class HitSkillEffect : SkillEffect
{
    [Tooltip("피격 또는 타격 위치에서 순간적으로 생성되었다가 사라질 폭발 이펙트 프리팹을 지정합니다.")]
    [SerializeField] private GameObject m_hitEffectPrefab;

    public override bool IsHitEffect => true;
    public override GameObject HitEffectPrefab => m_hitEffectPrefab;
}
