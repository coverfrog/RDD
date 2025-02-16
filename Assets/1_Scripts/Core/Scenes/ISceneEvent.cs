using System;

namespace Cf.Scenes
{
    public interface ISceneEvent<T> where T : Enum
    {
        public void OnRequestEvent(T eventType);
    }
}
