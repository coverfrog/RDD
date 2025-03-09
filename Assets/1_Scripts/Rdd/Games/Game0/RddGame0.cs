using System;
using System.Collections;
using UnityEngine;

public class RddGame0 : MonoBehaviour
{
    public event Action<RddGame0Data> OnDataChange;
    
    public RddGame0Data Data
    {
        get => _mData;
        set
        {
            _mData = value;
            
            OnDataChange?.Invoke(_mData);
        }
    }
    
    // ::

    public static event Action<float> OnRoundTimer; 
    
    // ::
    
    private RddGame0Data _mData = new RddGame0Data();
    private RddGame0RoundDataGroup _mRoundDataGroup;

    private IEnumerator _coRound;
    
    // ::

    private void Awake()
    {
        _mRoundDataGroup = Resources.Load<RddGame0RoundDataGroup>("Game0/Rdd Game 0 Round Data Group");

        Data.isLoad = true;

    }

    private void Start()
    {
        OnDataChange?.Invoke(_mData);
    }

    public void RoundDebugStart(int round)
    {
        Data.isDebug = true;

        RoundStart(round);
    }

    private void RoundStart(int round = 0)
    {
        if (_coRound != null)
        {
            return;
        }
        
        var roundData = _mRoundDataGroup.List[round];

        _coRound = CoRound(roundData);
        StartCoroutine(_coRound);
    }

    private IEnumerator CoRound(RddGame0RoundData data)
    {
        for (float t = data.Duration; t >= 0.0f ; t -= Time.deltaTime)
        {
            OnRoundTimer?.Invoke(t);
            
            yield return null;
        }
        
        OnRoundTimer?.Invoke(0);
    }
}
