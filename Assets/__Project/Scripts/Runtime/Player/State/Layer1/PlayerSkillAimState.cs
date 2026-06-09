using UnityEngine;

public class PlayerSkillAimState : PlayerState
{
    public override void Enter()
    {
        SkillContext context = Owner.CurrentSkillContext;
        context.TriggerSkillCast = false;
        context.CancelSkillCast = false;

        // 클라이언트 데코레이션: 조준선(범위 인디케이터) 표시
        Debug.Log($"[Aim] Show Range Indicator for Skill {Owner.CurrentSkillContext.ActiveSkillSlot} (Mode: {Owner.CurrentSkillContext.ActiveCastingMode})");
    }

    public override void Exit()
    {
        // 조준선 비활성화
        Debug.Log($"[Aim] Hide Range Indicator");
    }

    public override void Update()
    {
        int slot = Owner.CurrentSkillContext.ActiveSkillSlot;
        if (slot < 0) return;

        SkillContext skilContext = Owner.CurrentSkillContext;

        // 마우스 우클릭 시 조준 취소 (Normal, QuickOnRelease 공통)
        if (Owner.CurrentInputContext.IsClickRight)
        {
            skilContext.CancelSkillCast = true;
            
            Owner.CurrentSkillContext = skilContext;

            return;
        }
        if (Owner.CurrentSkillContext.ActiveCastingMode == CastingMode.Normal)
        {
            // 일반 시전: 마우스 좌클릭 시 시전 확정
            if (Owner.CurrentInputContext.IsClickLeft)
            {
                skilContext.SkillTargetPoint = GetMouseGroundPoint();
                skilContext.TriggerSkillCast = true;

                Owner.CurrentSkillContext = skilContext;
            }
        }
        else if (Owner.CurrentSkillContext.ActiveCastingMode == CastingMode.QuickOnRelease)
        {
            // 키 떼기 시전: 슬롯 단축키에서 손을 떼었을 때 시전 확정
            if (Owner.CurrentInputContext.GetSlotClicked(slot) == false)
            {
                skilContext.SkillTargetPoint = GetMouseGroundPoint();
                skilContext.TriggerSkillCast = true;

                Owner.CurrentSkillContext = skilContext;
            }
        }
    }

    private Vector3 GetMouseGroundPoint()
    {
        if (UtilWorld.TryGetMouseGroundPoint(out Vector3 point))
        {
            return point;
        }
        return Owner.transform.position;
    }
}
