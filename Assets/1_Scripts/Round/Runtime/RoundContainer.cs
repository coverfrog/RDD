using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundContainer
{
    // 난이도에 따른 So 데이터
    private readonly Dictionary<RoundDifficulty, List<RoundSoData>> _mSoDataListDict;
    
    
    // 현재 데이터 
    public RoundData RoundNowData { get; private set; }

    
    // 생성자
    public RoundContainer(List<RoundSoData> soDataList)
    {
        // 난이도에 따른 데이터 생성
        _mSoDataListDict = new Dictionary<RoundDifficulty, List<RoundSoData>>();
        
        IEnumerable<IGrouping<RoundDifficulty, RoundSoData>> soDataGroup = soDataList.GroupBy(sd => sd.Difficulty);
        foreach (IGrouping<RoundDifficulty, RoundSoData> grouping in soDataGroup)
        {
            RoundDifficulty key = grouping.Key;
            List<RoundSoData> value = new List<RoundSoData>();
            
            foreach (RoundSoData roundSoData in grouping)
            {
                value.Add(roundSoData);
            }
            
            _mSoDataListDict.Add(key, value);
        }
        
        // 현재 데이터
        RoundNowData = new RoundData
        {
            // Enum
            Difficulty           = null,
            
            // Bool
            IsEnd                = null,
            IsEndByDuration      = null,
            IsEndByHuntTarget    = null,
            
            // Int
            RemainEndByHuntCount = null,
            RemainEnemyCount     = null,
            
            // Float
            RemainDuration       = null,
            
            // String
            EndByHuntCodeName    = null
        };
    }

    public bool RoundSet(RoundDifficulty difficulty, int roundIdx)
    {
        // So 데이터
        if (!_mSoDataListDict.TryGetValue(difficulty, out List<RoundSoData> roundDataList))
        {
            return false;
        }

        if (roundIdx < 0 || roundIdx >= roundDataList.Count)
        {
            return false;
        }

        RoundSoData soData = roundDataList[roundIdx];
        
        // 현재 데이터
        // Enum
        RoundNowData.Difficulty           = difficulty;
        
        // Bool
        RoundNowData.IsEnd                = false;
        RoundNowData.IsEndByDuration      = soData.IsEndByDuration;
        RoundNowData.IsEndByHuntTarget    = soData.IsEndByHuntTarget;
        
        // Int
        RoundNowData.RemainEnemyCount     = RoundNowData.RemainEnemyCount != null ? RoundNowData.RemainEnemyCount : 0;
        RoundNowData.RemainEndByHuntCount = RoundNowData.RemainEndByHuntCount;
        
        // Float
        RoundNowData.RemainDuration       = soData.EndByDuration;
        
        // String
        RoundNowData.EndByHuntCodeName    = soData.EndByHuntCodeName;
        
        // 데이터 세팅 완료
        return true;
    }

    public bool RoundYieldEndByDuration()
    {
        // 현재 데이터, 시간을 감소
        if (RoundNowData.RemainDuration != null)
        {
            RoundNowData.RemainDuration = Mathf.Max(0.0f, RoundNowData.RemainDuration.Value - Time.deltaTime);
        }
        
        // 성공 조건
        return RoundNowData.RemainDuration is <= 0;
    }
    
    public bool RoundYieldEndByHuntTarget()
    {
        // 성공 조건
        return RoundNowData.RemainEndByHuntCount is <= 0;
    }
}
