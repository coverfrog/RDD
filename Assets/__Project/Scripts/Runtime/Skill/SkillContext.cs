using UnityEngine;

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

    public CastingMode GetSkillCastingMode(int slotIndex)
    {
        // 예시: 0번 스킬은 일반 시전, 1번 스킬은 즉시 시전, 2번 스킬은 키 떼기 시전
        // 테스트 용도
        if (slotIndex == 0) 
            return CastingMode.Normal;
        if (slotIndex == 1) 
            return CastingMode.Quick;
        return CastingMode.QuickOnRelease;
    }
}
