using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputNetworkSender))]
[RequireComponent(typeof(StatCtrl))]
public class PlayerCtrl : NetworkBehaviour
{
    public InputContext CurrentInputContext;
    [SyncVar]
    public SkillContext CurrentSkillContext;

    public bool IsDashing => CurrentSkillContext.IsDashing;

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
                    if (Vector3.Distance(currentXZ, targetXZ) < 0.2f)
                        return true;

                    if (IsDashing)
                        return true;

                    return false;
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
                    if (isLocalPlayer == false)
                        return false;

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
                    if (isLocalPlayer == false)
                        return false;

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
                m_smGroup.AddTransition(1, "Aim", "Cast", () => isLocalPlayer == true && CurrentSkillContext.TriggerSkillCast);

                // 조준 -> 취소
                m_smGroup.AddTransition(1, "Aim", "None", () => isLocalPlayer == true && CurrentSkillContext.CancelSkillCast);

                // 시전 완료 ➔ 대기 복귀
                m_smGroup.AddTransition(1, "Cast", "None", () => isLocalPlayer == true && CurrentSkillContext.IsSkillCastingFinished);

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
            RuntimeDataArr = new SkillRuntimeData[4]
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
        if (isServer && CurrentSkillContext.IsDashing)
        {
            if (Time.time >= CurrentSkillContext.DashEndTime)
            {
                SkillContext context = CurrentSkillContext;
                context.IsDashing = false;
                CurrentSkillContext = context;

                Vector3 vel = Rb3d.linearVelocity;
                Rb3d.linearVelocity = new Vector3(0, vel.y, 0);
            }
            else
            {
                Vector3 vel = Rb3d.linearVelocity;
                Rb3d.linearVelocity = new Vector3(CurrentSkillContext.DashDirection.x * CurrentSkillContext.DashSpeed, vel.y, CurrentSkillContext.DashDirection.z * CurrentSkillContext.DashSpeed);
                if (CurrentSkillContext.DashDirection.sqrMagnitude > 0.001f)
                {
                    transform.rotation = Quaternion.LookRotation(CurrentSkillContext.DashDirection);
                }
            }
        }

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
    public void CmdSpawnProjectile(ulong skillId, int level, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (DataManager.Instance == null || !DataManager.Instance.IsLoaded)
        {
            Debug.LogError("[PlayerCtrl] DataManager is not initialized yet!");
            return;
        }

        if (!DataManager.Instance.SkillInfos.TryGetValue(skillId, out SkillData skillData))
        {
            Debug.LogError($"[PlayerCtrl] SkillData not found for ID: {skillId}");
            return;
        }

        SkillLevelData levelData = skillData.GetLevelData(level);
        if (levelData == null || levelData.SkillEffect == null)
        {
            Debug.LogError($"[PlayerCtrl] SkillEffect not found for Skill ID: {skillId}, Level: {level}");
            return;
        }

        SkillEffect skillEffect = levelData.SkillEffect;
        if (!skillEffect.IsProjectile || skillEffect.ProjectilePrefab == null)
        {
            Debug.LogError($"[PlayerCtrl] Skill ID: {skillId} is not a projectile skill or prefab is null");
            return;
        }

        ProjectileCtrl prefab = skillEffect.ProjectilePrefab;
        float speed = levelData.Speed;

        ProjectileCtrl instance = Instantiate(prefab, spawnPosition, spawnRotation);
        instance.Setup(this, speed);
        NetworkServer.Spawn(instance.gameObject, connectionToClient);
    }

    [Command]
    public void CmdStartDash(Vector3 direction, float speed, float duration)
    {
        SkillContext context = CurrentSkillContext;
        context.IsDashing = true;
        context.DashDirection = direction.normalized;
        context.DashSpeed = speed;
        context.DashEndTime = Time.time + duration;
        CurrentSkillContext = context;
    }

    [Command]
    public void CmdStopDash()
    {
        SkillContext context = CurrentSkillContext;
        context.IsDashing = false;
        CurrentSkillContext = context;

        Vector3 vel = Rb3d.linearVelocity;
        Rb3d.linearVelocity = new Vector3(0, vel.y, 0);
    }

    #endregion
}
