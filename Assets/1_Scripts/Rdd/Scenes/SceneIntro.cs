using System;
using System.Collections;
using Cf.Scenes;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneIntro : SceneHandler
{
    private SceneAddressableLoader _loader;
    
    protected override IEnumerator Start()
    {
        yield return base.Start();

        _loader = AddressableLoad(
            "Rdd_Scene_MainMenu",
            null,
            null,
            null,
            LoadSceneMode.Additive,
            false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _loader.Active();
        }
    }
}
