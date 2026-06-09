using UnityEngine;

public class PlayerSkillNoneState : PlayerState
{
    public override void Enter()
    {
        SkillContext context = Owner.CurrentSkillContext;
        context.ActiveSkillSlot = -1;
        context.TriggerSkillCast = false;
        context.CancelSkillCast = false;
        context.IsSkillCastingFinished = false;

        Owner.CurrentSkillContext = context;
    }
}
