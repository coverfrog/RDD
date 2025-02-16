using System;
using Cf.Scenes;
using UnityEngine;

public abstract class RddSceneHandler : SceneHandler
{
    [SerializeField] [TextArea(6, 100)] private string mDescription;
    
    public abstract void OnProjectBegin();

    protected void OnEnable()
    {
        RddManager.Instance.SceneHandler = this;
    }
}
