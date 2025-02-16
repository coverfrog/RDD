using System;
using System.Collections.Generic;
using Cf.Scenes;
using UnityEngine;
using Object = UnityEngine.Object;

public class SceneHandlerGame0 : SceneHandler<SceneEventGame0>
{
    [Header("Reference")] 
    [SerializeField] private List<SceneEventBehaviour<SceneEventGame0>> mEventBehaviourList;
    
    #region :: Event

    public override void RequestEvent(Object sender, SceneEventGame0 eventType)
    {
        // act -> this
        Action requestAction = eventType switch
        {
            SceneEventGame0.Init => OnEventInit,
            SceneEventGame0.StartWait => OnEventStartWait,
            SceneEventGame0.Start => OnEventStart,
            _ => null,
        };
        
        requestAction?.Invoke();
        
        // act -> others
        if (requestAction == null)
        {
            return;
        }
        
        foreach (SceneEventBehaviour<SceneEventGame0> sceneEventBehaviour in mEventBehaviourList)
        {
            sceneEventBehaviour.OnRequestEvent(eventType);
        }
    }

    private void OnEventInit()
    {
        
    }

    private void OnEventStartWait()
    {
        
    }

    private void OnEventStart()
    {
        
    }

    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) RequestEvent(this, SceneEventGame0.Init);
        if (Input.GetKeyDown(KeyCode.Alpha2)) RequestEvent(this, SceneEventGame0.StartWait);
        if (Input.GetKeyDown(KeyCode.Alpha3)) RequestEvent(this, SceneEventGame0.Start);
    }
}
