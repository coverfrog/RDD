using UnityEngine;

public class PlayerSkillUseState : PlayerState
{
    private float m_duration;
    private float m_startTime;

    public override void Enter()
    {
        m_startTime = Time.time;
        m_duration = GetSkillDuration(Owner.CurrentSkillContext.ActiveSkillSlot);

        SkillContext context = Owner.CurrentSkillContext;
        context.IsSkillCastingFinished = false;

        Owner.CurrentSkillContext = context;

        Debug.Log($"[Cast] Skill {Owner.CurrentSkillContext.ActiveSkillSlot} casted at {Owner.CurrentSkillContext.SkillTargetPoint}!");

        // TODO: 실제 서버 스킬 효과 발동 및 클라이언트 이펙트/사운드 재생
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

    private float GetSkillDuration(int slot)
    {
        switch (slot)
        {
            case 0: return 1.0f;
            case 1: return 1.5f;
            case 2: return 0.8f;
            case 3: return 2.0f;
            default: return 1.0f;
        }
    }
}