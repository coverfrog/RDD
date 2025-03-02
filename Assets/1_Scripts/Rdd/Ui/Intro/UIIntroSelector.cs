using System;
using System.Collections.Generic;
using UnityEngine;

public class UIIntroSelector : MonoBehaviour
{
    [Header("Reference")] 
    [SerializeField] private List<UIIntroSelect> mUIIntroSelectList;

    private bool _mIsClickEventRun;

    private void OnEnable()
    {
        _mIsClickEventRun = false;
    }

    public void OnClickSelect(UIIntroSelect select)
    {
        if (_mIsClickEventRun)
        {
            return;
        }
        
        select.OnInteract(this);

        _mIsClickEventRun = true;
    }

    public void OnClickEventComplete()
    {
        _mIsClickEventRun = false;
    }
}
