using UnityEngine;

public sealed class SceneHandlerGame0 : SceneHandler
{
    public override void OnSendCommand(InputCommandName commandName)
    {
        Debug.Log(commandName);
    }
}
