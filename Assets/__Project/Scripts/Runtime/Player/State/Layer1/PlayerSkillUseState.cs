using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillUseState : PlayerState
{
    public override void Enter()
    {
        // : Context 갱신

        SkillContext context = Owner.CurrentSkillContext;
        context.IsSkillCastingFinished = false;
        context.IsDashing = false;
        context.UseStartTime = NetworkTime.time;

        if (Owner.CurrentSkillContext.TryGetSlotSkillDuration(Owner, out float duration))
        {
            context.UseDuration = duration;
        }

        Owner.CurrentSkillContext = context;

        // : Skill 사용

        UseSkill();
    }

    public override void Update()
    {
        if (Owner.isServer == false)
        {
            return;
        }

        if (NetworkTime.time - Owner.CurrentSkillContext.UseStartTime >= Owner.CurrentSkillContext.UseDuration)
        {
            SkillContext context = Owner.CurrentSkillContext;
            context.IsSkillCastingFinished = true;

            Owner.CurrentSkillContext = context;
        }
    }

    public override void FixedUpdate()
    {
        if (Owner.CurrentSkillContext.IsDashing == true &&
            Owner.isLocalPlayer == true &&
            Owner.Rb3d.isKinematic == false)
        {
            Vector3 currentVel = Owner.Rb3d.linearVelocity;
            Vector3 dir = Owner.CurrentSkillContext.DashDirection;
            float speed = Owner.CurrentSkillContext.DashSpeed;
            Owner.Rb3d.linearVelocity = new Vector3(dir.x * speed, currentVel.y, dir.z * speed);
        }
    }

    public override void Exit()
    {
        if (Owner.isLocalPlayer == true)
        {
            if (Owner.Rb3d.isKinematic == false &&
                Owner.CurrentSkillContext.IsDashing == true)
            {
                Vector3 currentVel = Owner.Rb3d.linearVelocity;
                Owner.Rb3d.linearVelocity = new Vector3(0, currentVel.y, 0);
            }
            
            SkillContext context = Owner.CurrentSkillContext;
            context.IsDashing = false;

            Owner.CurrentSkillContext = context;
            Owner.CmdStopDash();
        }
    }

    private void UseSkill()
    {
        if (Owner.CurrentSkillContext.TryGetSlotSkill(Owner, out SkillData skillData, out SkillLevelData levelData) == false)
        {
            return;
        }

        if (Owner.isLocalPlayer == false)
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
                    Owner.CurrentSkillContext.ActiveSkillSlot,
                    skillData.ID,
                    levelData.Level,
                    Owner.transform.position,
                    rotation);
            }

            if (skillEffect.IsDash)
            {
                SkillContext context = Owner.CurrentSkillContext;
                context.IsDashing = true;
                context.DashSpeed = levelData.Speed;

                Vector3 direction = context.SkillTargetPoint - Owner.transform.position;
                direction.y = 0;

                if (direction.sqrMagnitude < 0.001f)
                {
                    direction = Owner.transform.forward;
                }

                context.DashDirection = direction.normalized;
                Owner.CurrentSkillContext = context;

                // Rotate player to face the dash direction immediately
                Owner.transform.rotation = Quaternion.LookRotation(context.DashDirection);

                // Start dash on the server
                Owner.CmdStartDash(context.ActiveSkillSlot, context.DashDirection, context.DashSpeed, context.UseDuration);
            }

            if (skillEffect.IsSurroundingEffect)
            {

            }

            if (skillEffect.IsHitEffect)
            {

            }
        }
    }
}