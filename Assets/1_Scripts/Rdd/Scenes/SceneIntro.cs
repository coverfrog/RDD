using System;
using System.Collections;
using Cf.Scenes;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneIntro : SceneHandler
{
    public static void SceneToMainMenu(UIIntroSelector selector)
    {
        AddressableLoad("Rdd_Scene_Game", null, null, null);
    }
}
