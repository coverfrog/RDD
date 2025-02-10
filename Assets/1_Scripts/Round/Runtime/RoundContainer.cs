using System;
using System.Collections.Generic;
using UnityEngine;

public class RoundContainer
{
    // 원형 데이터
    private List<RoundSoData> _mSoDataList;

    // 연산자
    private RoundCore _mCore;
    
    // 현재 데이터
    private uint _mRound;

    public RoundContainer(List<RoundSoData> soDataList)
    {
        // 원형 데이터
        _mSoDataList = soDataList;

        // 연산자
        _mCore = new RoundCore();
        
        // 현재 데이터
        _mRound = 0;
    }
}
