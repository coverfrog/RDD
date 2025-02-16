using Cf.Scenes;
using UnityEngine;

public class CuteBirdHelper : SceneEventBehaviour<SceneEventGame0>
{
    public override void OnRequestEvent(SceneEventGame0 eventType)
    {
        Debug.Log(eventType);
    }
}
