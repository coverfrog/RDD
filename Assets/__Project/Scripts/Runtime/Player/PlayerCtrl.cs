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

    public bool IsSkillOnCooldown(int slot)
    {
        if (CurrentSkillContext.RuntimeDataArr == null || slot < 0 || slot >= CurrentSkillContext.RuntimeDataArr.Length)
            return false;

        return NetworkTime.time < CurrentSkillContext.RuntimeDataArr[slot].CooldownEndTime;
    }

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
                m_smGroup.AddState(0, "Dash", new PlayerDashState());

                m_smGroup.AddTransition(0, "Idle", "Move", () =>
                    CurrentInputContext.IsClickRight);

                m_smGroup.AddTransition(0, "Move", "Idle", () =>
                {
                    Vector3 currentXZ = new Vector3(transform.position.x, 0, transform.position.z);
                    Vector3 targetXZ = new Vector3(CurrentInputContext.MoveGroundPoint.x, 0, CurrentInputContext.MoveGroundPoint.z);
                    return Vector3.Distance(currentXZ, targetXZ) < 0.2f;
                });

                m_smGroup.AddTransition(0, "Move", "Dash", () =>
                {
                    return CurrentSkillContext.IsDashing;
                });

                m_smGroup.AddTransition(0, "Dash", "Idle", () =>
                {
                    return true;
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
                            if (IsSkillOnCooldown(i))
                                continue;

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
                            if (IsSkillOnCooldown(i))
                                continue;

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
        if (isServer)
        {
            if (CurrentSkillContext.ActiveSkillSlot != -1 && CurrentSkillContext.IsSkillCastingFinished == false)
            {
                if (NetworkTime.time >= CurrentSkillContext.UseStartTime + CurrentSkillContext.UseDuration)
                {
                    SkillContext context = CurrentSkillContext;
                    context.IsSkillCastingFinished = true;
                    context.ActiveSkillSlot = -1;
                    CurrentSkillContext = context;
                    Debug.Log($"[Server] Casting finished. Reset active slot to -1.");
                }
            }
        }

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

    #region : Skill Helper Methods (Server Only)

    /// <summary>
    /// 서버 권한으로 데이터 매니저 상태, 스킬 데이터 존재 여부, 쿨다운 상태를 종합 검증합니다.
    /// </summary>
    private bool ValidateAndCheckCooldown(int slot, ulong skillId, int level, out SkillLevelData levelData)
    {
        levelData = null;

        // 1. 데이터 매니저 로딩 확인
        if (DataManager.Instance == null || !DataManager.Instance.IsLoaded)
        {
            Debug.LogError("[PlayerCtrl] DataManager is not initialized yet!");
            return false;
        }

        // 2. 슬롯 범위 확인
        if (slot < 0 || slot >= CurrentSkillContext.RuntimeDataArr.Length)
        {
            Debug.LogError($"[PlayerCtrl] Invalid slot index: {slot}");
            return false;
        }

        // 3. 스킬 기본 데이터 확인
        if (!DataManager.Instance.SkillInfos.TryGetValue(skillId, out SkillData skillData))
        {
            Debug.LogError($"[PlayerCtrl] SkillData not found for ID: {skillId}");
            return false;
        }

        // 4. 레벨별 데이터 및 이펙트 확인
        levelData = skillData.GetLevelData(level);
        if (levelData == null)
        {
            Debug.LogError($"[PlayerCtrl] SkillLevelData not found for ID: {skillId}, Level: {level}");
            return false;
        }

        // 5. 서버 authoritative 쿨다운 검사 (0.1초 보정치 포함)
        if (NetworkTime.time + 0.1 < CurrentSkillContext.RuntimeDataArr[slot].CooldownEndTime)
        {
            Debug.LogWarning($"[PlayerCtrl] Command rejected: Skill ID {skillId} is on cooldown on server. CurrentTime={NetworkTime.time}, CooldownEndTime={CurrentSkillContext.RuntimeDataArr[slot].CooldownEndTime}");

            // 거부되었으므로 클라이언트 상태를 강제로 풀어줌
            ForceUnlockClient();
            return false;
        }

        return true;
    }

    /// <summary>
    /// 쿨다운 거부 또는 스킬 예외 발생 시 클라이언트의 Cast 상태를 강제 해제합니다.
    /// </summary>
    private void ForceUnlockClient()
    {
        SkillContext rejectContext = CurrentSkillContext;
        rejectContext.IsSkillCastingFinished = true;
        rejectContext.ActiveSkillSlot = -1;
        CurrentSkillContext = rejectContext;
    }

    /// <summary>
    /// 스킬 시전 시작에 따른 구조체 데이터를 깊은 복사하여 갱신하고 쿨다운을 적용합니다.
    /// </summary>
    private void InitializeServerSkillCast(int slot, float cooldown, float duration)
    {
        SkillContext context = CurrentSkillContext;

        // 구조체 내부 배열의 참조 전염을 막기 위한 Clone
        context.RuntimeDataArr = (SkillRuntimeData[])context.RuntimeDataArr.Clone();
        context.RuntimeDataArr[slot].CooldownEndTime = NetworkTime.time + cooldown;

        // 시전 상태 초기화 및 지속시간 설정
        context.ActiveSkillSlot = slot;
        context.IsSkillCastingFinished = false;
        context.UseStartTime = NetworkTime.time;
        context.UseDuration = duration;

        CurrentSkillContext = context;
    }

    #endregion

    #region : Skill Methods (Server Only)

    [Command]
    public void CmdSpawnProjectile(int slot, ulong skillId, int level, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        Debug.Log($"[Server] CmdSpawnProjectile called: slot={slot}, skillId={skillId}, level={level}");

        // 공통 검증 및 쿨다운 체크
        if (!ValidateAndCheckCooldown(slot, skillId, level, out SkillLevelData levelData))
            return;

        if (levelData.SkillEffect == null)
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

        // 공통 상태 변경 및 쿨다운 적용
        InitializeServerSkillCast(slot, levelData.Cooldown, levelData.Duration);
        Debug.Log($"[Server] Cooldown updated for slot {slot}: CooldownEndTime={CurrentSkillContext.RuntimeDataArr[slot].CooldownEndTime}");

        // 발사체 생성
        ProjectileCtrl prefab = skillEffect.ProjectilePrefab;
        float speed = levelData.Speed;

        Debug.Log($"[Server] Instantiating projectile prefab: {prefab.name} at {spawnPosition} with speed {speed}");
        ProjectileCtrl instance = Instantiate(prefab, spawnPosition, spawnRotation);
        instance.Setup(this, speed);
        NetworkServer.Spawn(instance.gameObject, connectionToClient);
        Debug.Log("[Server] Projectile successfully spawned via NetworkServer.");
    }

    [Command]
    public void CmdStartDash(int slot, Vector3 direction, float speed, float duration)
    {
        // Dash 스킬 역시 ID를 역추적하기 위해 현재 슬롯 정보 활용
        if (slot < 0 || slot >= CurrentSkillContext.RuntimeDataArr.Length) return;
        ulong skillId = (ulong)CurrentSkillContext.RuntimeDataArr[slot].ID;
        int level = CurrentSkillContext.RuntimeDataArr[slot].Level;

        // 공통 검증 및 쿨다운 체크
        if (!ValidateAndCheckCooldown(slot, skillId, level, out SkillLevelData levelData))
            return;

        // 대시 고유 데이터 셋업
        SkillContext context = CurrentSkillContext;
        context.IsDashing = true;
        context.DashDirection = direction.normalized;
        context.DashSpeed = speed;
        context.DashEndTime = Time.time + duration;
        CurrentSkillContext = context;

        // 공통 상태 변경 및 쿨다운 적용 (대시는 인자로 받은 duration 사용)
        InitializeServerSkillCast(slot, levelData.Cooldown, duration);
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
