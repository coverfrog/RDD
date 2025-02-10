using System;
using System.Collections.Generic;
using UnityEngine;

public class RoundHandler : MonoBehaviour
{
    [SerializeField] private List<RoundSoData> mRoundSoDataList;

    private RoundContainer _mRoundContainer;
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _mRoundContainer = new RoundContainer(mRoundSoDataList);
    }
}
