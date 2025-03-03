using System;
using System.Collections.Generic;
using Cf.Docs;
using UnityEngine;

public class RoundHandler : MonoBehaviour
{
    public void RoundStartFirst(UICount uiCount)
    {
        uiCount.gameObject.SetActive(false);
        
        RoundStart(0);
    }

    private void RoundStart(int idx)
    {
        Debug.Log($"idx : {idx}");
    }
}
