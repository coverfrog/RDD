using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SkillContext 
{
    /// <summary>
    /// 활성화할 스킬 슬롯
    /// </summary>
    public int ActiveSkillSlot;

    /// <summary>
    /// 캐스팅 모드
    /// </summary>
    public CastingMode ActiveCastingMode;

    /// <summary>
    /// 스킬 날릴 위치
    /// </summary>
    public Vector3 SkillTargetPoint;

    /// <summary>
    /// 스킬 캐스팅 트리거
    /// </summary>
    public bool TriggerSkillCast;

    /// <summary>
    /// 스킬 취소
    /// </summary>
    public bool CancelSkillCast;

    /// <summary>
    /// 스킬 완료 
    /// </summary>
    public bool IsSkillCastingFinished;

    /// <summary>
    /// 런타임
    /// </summary>
    public SkillRuntimeData[] RuntimeInfos;

    public bool TryGetSkillCastingMode(PlayerCtrl owner, out CastingMode castingMode)
    {
        if (TryGetSlotSkill(owner, out SkillData skillData, out _))
        {
            castingMode = skillData.CastingMode;
            return true;
        }

        castingMode = CastingMode.Quick;

        return false;
    }

    public bool TryGetSlotSkillDuration(PlayerCtrl owner, out float duration)
    {
        if (TryGetSlotSkill(owner, out _, out SkillLevelData levelData))
        {
            duration = levelData.Duration;
            return true;
        }

        duration = -1.0f;

        return false;
    }

    public bool TryGetSlotSkill(PlayerCtrl owner, out SkillData skillData, out SkillLevelData levelData)
    {
        int slot = ActiveSkillSlot;

        SkillRuntimeData[] runtimeDataArr = owner.CurrentSkillContext.RuntimeInfos;
        if (runtimeDataArr.Length == 0 || slot > runtimeDataArr.Length - 1)
        {
            Debug.LogError($"Error");

            skillData = null;
            levelData = null;

            return false;
        }

        SkillRuntimeData runtimeInfo = runtimeDataArr[slot];
        if (runtimeInfo.Level == 0)
        {
            Debug.LogError("Error");

            skillData = null;
            levelData = null;

            return false;
        }

        if (DataManager.Instance.SkillInfos.TryGetValue(runtimeInfo.ID, out skillData) == false)
        {
            skillData = null;
            levelData = null;

            return false;
        }

        List<SkillLevelData> levelDataList = skillData.LevelDataList;
        if (runtimeInfo.Level > levelDataList.Count)
        {
            skillData = null;
            levelData = null;

            return false;
        }

        levelData = levelDataList[runtimeInfo.Level - 1];

        return true;
    }
}
