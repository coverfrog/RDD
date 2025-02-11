using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundHandler : MonoBehaviour
{
    [Header("Option")] 
    [SerializeField] private bool mAutoStart;
    
    
    [Header("So")]
    [SerializeField] private List<RoundSoData> mRoundSoDataList;

    
    [Header("Event")] 
    [SerializeField] private UnityEvent<RoundData> mRoundBeginEvent;
    [SerializeField] private UnityEvent<RoundData> mRoundingEvent;
    [SerializeField] private UnityEvent<RoundData> mRoundEndEvent;

    
    private RoundContainer _mRoundContainer;


    private IEnumerator _mCoRound;
    
    
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        if (mAutoStart)
        {
            RoundBegin(RoundDifficulty.Lv0, 0);
        }
    }

    private void Init()
    {
        _mRoundContainer = new RoundContainer(mRoundSoDataList);
    }

    private void RoundBegin(RoundDifficulty difficulty, int roundIdx)
    {
        // 실행 여부
        if (_mCoRound != null)
        {
            return;
        }
        
        // 정보 세팅
        if (!_mRoundContainer.RoundSet(difficulty, roundIdx))
        {
            return;
        }

        // 시작
        _mCoRound = CoRoundMethod();
        StartCoroutine(_mCoRound);
    }

    private IEnumerator CoRoundMethod()
    {
        // 현재 데이터
        RoundData roundNowData = _mRoundContainer.RoundNowData;
        
        // 종료 조건에 따른 Yield Func 획득
        List<Func<bool>> roundYieldFuncList = new List<Func<bool>>();

        if (roundNowData.IsEndByDuration is true)
        {
            roundYieldFuncList.Add(_mRoundContainer.RoundYieldEndByDuration);
        }

        if (roundNowData.IsEndByHuntTarget is true)
        {
            roundYieldFuncList.Add(_mRoundContainer.RoundYieldEndByHuntTarget);
        }


        // 종료 조건 없을시 바로 종료
        // 코루틴 할당 해제
        if (roundYieldFuncList.Count <= 0)
        {
            _mCoRound = null;
            
            yield break;
        }

        // 종료 조건 전까지 대기
        // 시작시 이벤트
        // 종료시 이벤트
        mRoundBeginEvent?.Invoke(roundNowData);
        
        while (roundNowData.IsEnd is false)
        {
            mRoundingEvent?.Invoke(roundNowData);

            bool isEnd = false;
            
            foreach (var roundYieldFunc in roundYieldFuncList)
            {
                if (roundYieldFunc.Invoke())
                {
                    isEnd = true;
                    break;
                }
            }

            if (isEnd)
            {
                roundNowData.IsEnd = true;
            }

            yield return null;
        }
        
        mRoundEndEvent?.Invoke(roundNowData);
        
        // 코루틴 할당 해제
        _mCoRound = null;
    }
}
