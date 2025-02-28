using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cf.Pattern;

public partial class RddManager : Singleton<RddManager>
{
    private const bool IsOnStart = true;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnProjectStart()
    {
        _ = Instance;
    }

    private void Start()
    {
        if (!IsOnStart)
        {
            return;
        }

        ScenarioSet();
        ScenarioStart();
    }

    private void Update()
    {
        UpdateDebug();
    }
}
