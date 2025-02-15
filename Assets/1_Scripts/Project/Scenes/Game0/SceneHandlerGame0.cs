using System;
using Cf.Scenes;
using Object = UnityEngine.Object;

public class SceneHandlerGame0 : SceneHandler<SceneEventGame0>
{
    public override void RequestEvent(Object sender, SceneEventGame0 tEnum)
    {
        Action requestAction = tEnum switch
        {
            SceneEventGame0.Init => OnEventInit,
            SceneEventGame0.StartWait => OnEventStartWait,
            SceneEventGame0.Start => OnEventStart,
            _ => null,
        };
        
        requestAction?.Invoke();
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
}
