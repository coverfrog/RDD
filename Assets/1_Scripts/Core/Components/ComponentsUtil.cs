using UnityEngine;

namespace Cf.Components
{
    public static class ComponentsUtil
    {
        public static void TryAddComponent<T>(Behaviour behaviour, out T t) where T : Component
        {
            if (behaviour.TryGetComponent(out t))
            {
                return;
            }

            t = behaviour.gameObject.AddComponent<T>();
        }
    }
}
