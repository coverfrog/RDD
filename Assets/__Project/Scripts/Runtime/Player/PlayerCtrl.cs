using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputNetworkSender))]
[RequireComponent(typeof(StatCtrl))]
public class PlayerCtrl : NetworkBehaviour
{
    public InputContext CurrentInputContext;
    public SkillContext CurrentSkillContext;

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

    #region : StatCtrl


    public StatCtrl StatCtrl
    {
        get
        {
            if (m_statCtrl == null) m_statCtrl = GetComponent<StatCtrl>();
            return m_statCtrl;
        }
    }

    internal StatCtrl m_statCtrl;

    #endregion

    #region : StateMachineGroup

    public StateMachineGroup<PlayerCtrl> SmGroup
    {
        get
        {
            if (m_smGroup == null)
            {
                m_smGroup = new StateMachineGroup<PlayerCtrl>(this);

                #region : Layer0. Idle <-> Move

                m_smGroup.AddState(0, "Idle", new PlayerIdleState());
                m_smGroup.AddState(0, "Move", new PlayerMoveState());

                m_smGroup.AddTransition(0, "Idle", "Move", () =>
                    CurrentInputContext.IsClickRight);

                m_smGroup.AddTransition(0, "Move", "Idle", () =>
                {
                    Vector3 currentXZ = new Vector3(transform.position.x, 0, transform.position.z);
                    Vector3 targetXZ = new Vector3(CurrentInputContext.MoveGroundPoint.x, 0, CurrentInputContext.MoveGroundPoint.z);
                    return Vector3.Distance(currentXZ, targetXZ) < 0.2f;
                });

                #endregion

                #region : Layer1. Skill
                // Layer1: Skill
                m_smGroup.AddState(1, "None", new PlayerSkillNoneState());
                m_smGroup.AddState(1, "Aim", new PlayerSkillAimState());
                m_smGroup.AddState(1, "Cast", new PlayerSkillUseState());

                // 대기 -> 조준
                m_smGroup.AddTransition(1, "None", "Aim", () =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (CurrentInputContext.GetSlotClick(i))
                        {
                            SkillContext context = CurrentSkillContext;
                            context.ActiveSkillSlot = i;
                            if (context.TryGetSkillCastingMode(this, out CastingMode castingMode))
                            {
                                context.ActiveCastingMode = castingMode;
                            }

                            CurrentSkillContext = context;

                            if (CurrentSkillContext.ActiveCastingMode != CastingMode.Quick) 
                                return true;
                        }
                    }
                    return false;
                });

                // 대기 -> 즉시 시전
                m_smGroup.AddTransition(1, "None", "Cast", () =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (CurrentInputContext.GetSlotClick(i))
                        {
                            SkillContext context = CurrentSkillContext;
                            context.ActiveSkillSlot = i;
                            if (context.TryGetSkillCastingMode(this, out CastingMode castingMode))
                            {
                                context.ActiveCastingMode = castingMode;
                            }

                            CurrentSkillContext = context;

                            if (context.ActiveCastingMode == CastingMode.Quick)
                            {
                                if (UtilWorld.TryGetMouseGroundPoint(out Vector3 point))
                                {
                                    context.SkillTargetPoint = point;
                                }
                                else
                                {
                                    context.SkillTargetPoint = transform.position;
                                }

                                CurrentSkillContext = context;

                                return true;
                            }
                        }
                    }
                    return false;
                });

                // 조준 -> 시전
                m_smGroup.AddTransition(1, "Aim", "Cast", () => CurrentSkillContext.TriggerSkillCast);

                // 조준 -> 취소
                m_smGroup.AddTransition(1, "Aim", "None", () => CurrentSkillContext.CancelSkillCast);

                // 시전 완료 ➔ 대기 복귀
                m_smGroup.AddTransition(1, "Cast", "None", () => CurrentSkillContext.IsSkillCastingFinished);

                #endregion

                m_smGroup.Run();
            }

            return m_smGroup;
        }
    }

    internal StateMachineGroup<PlayerCtrl> m_smGroup;

    #endregion

    #region : Unity

    private void Awake()
    {
        _ = m_smGroup;

        CurrentInputContext = new();

        CurrentSkillContext = new SkillContext()
        {
            RuntimeInfos = new SkillRuntimeData[4]
        {
            new SkillRuntimeData()
            {
                ID = 1,
                Level = 1,
            },
            new SkillRuntimeData()
            {
                ID = 2,
                Level = 1,
            },
            new SkillRuntimeData()
            {
                ID = 3,
                Level = 1,
            },
            new SkillRuntimeData()
            {
                ID = 4,
                Level = 1,
            }
        }
        };
    }

    private void Update()
    {
        SmGroup.Update();
    }

    private void FixedUpdate()
    {
        SmGroup.FixedUpdate();
    }

    private void LateUpdate()
    {
        InputContext context = CurrentInputContext;
        context.ClearOneShotInputs();

        CurrentInputContext = context;
    }

    #endregion

    #region : Cmd (State 내부에선 직접 Cmd 호출이 불가)

    [Command]
    public void CmdSpawnProjectile(ProjectileCtrl prefab, Vector3 spawnPosition, Quaternion spawnRotation, float speed)
    {
        ProjectileCtrl instance = Instantiate(prefab, spawnPosition, spawnRotation);
        instance.Setup(speed);
        // NetworkServer.Spawn(instance.gameObject);
        NetworkServer.Spawn(instance.gameObject, connectionToClient);
    }

    #endregion
}
