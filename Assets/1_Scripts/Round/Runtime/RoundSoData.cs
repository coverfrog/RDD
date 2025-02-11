using UnityEngine;

[CreateAssetMenu(menuName = "Cf/Round", fileName = "Round")]
public class RoundSoData : ScriptableObject
{
    [Header("Enum")]
    [SerializeField] private RoundDifficulty mDifficulty;
    public RoundDifficulty Difficulty => mDifficulty;

    [Header("Bool")]
    [SerializeField] private bool mIsEndByDuration = true;
    [SerializeField] private bool mIsEndByHuntTarget = true;

    public bool IsEndByDuration => mIsEndByDuration;
    public bool IsEndByHuntTarget => mIsEndByHuntTarget;

    [Header("Int")]
    [SerializeField] [Min(0)] private int mEndByHuntCount;
    public int EndByHuntCount => mEndByHuntCount;
    
    [Header("Float")]
    [SerializeField] [Min(0)] private float mEndByDuration = 30.0f;
    public float EndByDuration => mEndByDuration;

    [Header("String")]
    [SerializeField] private string mEndByHuntCodeName = "All";

    public string EndByHuntCodeName => mEndByHuntCodeName;


}
