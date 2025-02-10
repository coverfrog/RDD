using System;
using System.Collections.Generic;
using UnityEngine;

public class RoundContainer
{
    private RoundCore _mCore;
    private List<RoundSoData> _mSoDataList;
    
    private uint _mRound;

    public RoundContainer(List<RoundSoData> soDataList)
    {
        _mCore = new RoundCore();
        _mSoDataList = soDataList;
    }
}
