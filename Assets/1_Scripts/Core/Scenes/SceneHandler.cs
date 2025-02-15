using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Cf.Scenes
{
    public abstract class SceneHandler<TEnum> : MonoBehaviour where TEnum : Enum
    {
        public abstract void RequestEvent(Object sender, TEnum tEnum);
    }
}
