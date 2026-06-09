using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillInfo", menuName = "RDD/SkillInfo")]
public class SkillInfo : ScriptableObject
{
    [SerializeField] private CastingMode m_castingMode;
    [SerializeField] private float m_duration;

    public CastingMode CastingMode => m_castingMode;
    public float Duration => m_duration;
}
