using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UICount : MonoBehaviour
{
    [Header("Option")] 
    [SerializeField] private int mTargetCount = 3;
    [SerializeField] private float mInterval = 1.0f;
    [SerializeField] private string mFormat = "{0}";
    
    [Header("Reference")]
    [SerializeField] private TMP_Text mText;

    [Header("Event")]
    [SerializeField] private UnityEvent mEndEvent;

    private bool _mIsComplete;
    private int _mCount;
    private float _mIntervalTimer;

    private void OnCountUpdate(int count)
    {
        mText.text = string.Format(mFormat, count);
    }

    private void OnEnable()
    {
        _mIsComplete = false;
        _mCount = mTargetCount;
        _mIntervalTimer = 0.0f;

        OnCountUpdate(mTargetCount);
    }

    private void Update()
    {
        if (_mIsComplete)
        {
            return;
        }

        _mIntervalTimer += Time.deltaTime;

        if (_mIntervalTimer < mInterval)
        {
            return;
        }

        _mIntervalTimer = 0.0f;
        _mCount = Mathf.Max(_mCount - 1, 0);
        
        OnCountUpdate(_mCount);

        if (_mCount != 0)
        {
            return;
        }

        _mIsComplete = true;
        
        mEndEvent?.Invoke();
    }
}
