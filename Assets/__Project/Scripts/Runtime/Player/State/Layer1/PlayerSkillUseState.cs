using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillUseState : PlayerState
{
    private float m_duration;
    private float m_startTime;

    public override void Enter()
    {
        // : 내부 정보 갱신

        if (Owner.CurrentSkillContext.TryGetSlotSkillDuration(Owner, out float duration))
        {
            m_duration = duration;
        }
        m_startTime = Time.time;

        // : Context 갱신

        SkillContext context = Owner.CurrentSkillContext;
        context.IsSkillCastingFinished = false;

        Owner.CurrentSkillContext = context;

        // : Skill 사용

        UseSkill();
    }

    public override void Update()
    {
        if (Time.time - m_startTime >= m_duration)
        {
            SkillContext context = Owner.CurrentSkillContext;
            context.IsSkillCastingFinished = true;

            Owner.CurrentSkillContext = context;
        }
    }

    private void UseSkill()
    {
        if (Owner.CurrentSkillContext.TryGetSlotSkill(Owner, out SkillData skillData, out SkillLevelData levelData) == false)
        {
            return;
        }

        // : Log
        Debug.Log($"skill use at {skillData.name}");

        SkillEffect skillEffect = levelData.SkillEffect;

        if (skillEffect != null)
        {
            if (skillEffect.IsProjectile && skillEffect.ProjectilePrefab)
            {
                Vector3 direction = Owner.CurrentSkillContext.SkillTargetPoint - Owner.transform.position;
                direction.y = 0;

                Quaternion rotation = direction.sqrMagnitude < 0.001f ? 
                    Quaternion.identity : 
                    Quaternion.LookRotation(direction.normalized);

                Owner.CmdSpawnProjectile(
                    skillEffect.ProjectilePrefab, 
                    Owner.transform.position,
                    rotation,
                    skillEffect.ProjectileSpeed);
            }

            if (skillEffect.HasSurroundingEffect)
            {

            }

            if (skillEffect.HasHitEffect)
            {

            }
        }
    }
}