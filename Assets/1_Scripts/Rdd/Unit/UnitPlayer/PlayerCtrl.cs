using System;
using UnityEngine;

public sealed class PlayerCtrl : UnitCtrl
{
    [Header("Id")] 
    [SerializeField] private string mPlayerUnityId;
    
    #region :: Unit Type
    
    public override UnitType GetUnitType => UnitType.ToPlayer;

    #endregion

    #region :: Player Unity Id

    private static int CallIdx = 0;

    private void SetPlayerUnityId(string playerUnityId)
    {
        mPlayerUnityId = playerUnityId;

        CallIdx++;
    }

    public string PlayerUnityId => mPlayerUnityId;

    #endregion

    #region :: Unity

    private void Start()
    {
        SetPlayerUnityId(CallIdx.ToString());
    }

    #endregion
}
