using System;
using UnityEngine;

public class RddSceneGame0 : RddSceneHandler
{
    public override void OnProjectBegin()
    {
        // user, get debug
        UserManager.Instance.LocalUserData = new UserData(true)
        {
            userName = "debug",
            i = 0,
        };
    }

    private void Start()
    {
     
    }
}
