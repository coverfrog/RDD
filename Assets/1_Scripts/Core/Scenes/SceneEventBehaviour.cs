using System;
using UnityEngine;

namespace Cf.Scenes
{
    public abstract class SceneEventBehaviour<T> : MonoBehaviour, ISceneEvent<T> where T : Enum
    {
        public abstract void OnRequestEvent(T eventType);
    }
}
