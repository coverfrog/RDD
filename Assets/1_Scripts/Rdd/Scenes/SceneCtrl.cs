using System;
using UnityEngine;

public enum SceneName
{
    Intro,
    Game0,
}

public abstract class SceneCtrl : MonoBehaviour
{
    public abstract SceneName GetSceneName { get; }

    public void OnEnable()
    {
        RddManager.Instance.SetCtrlScene(this);
    }
}
