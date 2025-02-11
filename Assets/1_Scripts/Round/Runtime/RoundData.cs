public class RoundData 
{
    // Enum
    public RoundDifficulty? Difficulty { get; set; }
    
    // Bool
    public bool? IsEnd { get; set; }
    public bool? IsEndByDuration { get; set; }
    public bool? IsEndByHuntTarget { get; set; }
    
    // Int
    public int? RemainEnemyCount { get; set; }
    public int? RemainEndByHuntCount { get; set; }
    
    // Float
    public float? RemainDuration { get; set; }
    
    // String
    public string EndByHuntCodeName { get; set; }
}
