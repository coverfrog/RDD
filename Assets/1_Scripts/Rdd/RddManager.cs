using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cf.Pattern;

public class RddManager : GenericSingleton<RddManager>
{
    public RddSceneHandler SceneHandler { get; set; }
    
    // call, project begin
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void ProjectBegin()
    {
        _ = Instance;
    }

    private void Start()
    {
        // on 
        SceneHandler?.OnProjectBegin();
    }
}
