using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneIntro : SceneCtrl
{
    public override SceneName GetSceneName => SceneName.Intro;
}
