using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillLevelData
{
    [ReadOnly]
    public int Level;
    public float Value;
    public float Speed;
    public float Duration;
    public float Cooldown;

    [SerializeReference, SubclassSelector]
    private SkillEffect m_skillEffect;
    public SkillEffect SkillEffect => m_skillEffect;
}

[CreateAssetMenu(fileName = "NewSkillInfo", menuName = "RDD/SkillInfo")]
public class SkillData : ScriptableObject
{
    [ReadOnly]
    [SerializeField] private ulong m_id;
    [SerializeField] private CastingMode m_castingMode;
    [SerializeField] private DamageMode m_damageMode;
    [SerializeField] private List<SkillLevelData> m_levelDataList = new List<SkillLevelData>();

    public ulong ID => m_id;

    public CastingMode CastingMode => m_castingMode;

    public DamageMode DamageMode => m_damageMode;

    public List<SkillLevelData> LevelDataList => m_levelDataList;

    public SkillLevelData GetLevelData(int level)
    {
        if (m_levelDataList == null || m_levelDataList.Count == 0)
        {
            Debug.LogWarning($"[SkillInfo] LevelDataList is empty for {name}");
            return default;
        }

        for (int i = 0; i < m_levelDataList.Count; i++)
        {
            if (m_levelDataList[i].Level == level)
            {
                return m_levelDataList[i];
            }
        }

        return m_levelDataList[m_levelDataList.Count - 1];
    }
}
