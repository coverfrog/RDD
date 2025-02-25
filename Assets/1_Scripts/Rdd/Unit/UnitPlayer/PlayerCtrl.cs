using System;
using UnityEngine;

public sealed class PlayerCtrl : UnitCtrl
{
    [Header("L1 : Option")] 
    [SerializeField] private string mPlayerUnityId;
    
    #region :: Unit Type
    
    public override UnitType GetUnitType => UnitType.ToPlayer;

    #endregion

    #region :: Player Unity Id

    private void SetPlayerUnityId(string playerUnityId)
    {
        mPlayerUnityId = playerUnityId;
    }

    public string PlayerUnityId => mPlayerUnityId;

    #endregion

    #region :: Unity

    private void Start()
    {
        SetPlayerUnityId(DateTime.Now.ToString("T"));
    }

    #endregion
}
