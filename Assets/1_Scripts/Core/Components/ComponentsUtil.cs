using UnityEngine;

namespace Cf.Components
{
    public static class ComponentsUtil
    {
        public static void TryAddComponent<T>(Behaviour behaviour, out T t, bool isAllowOverlap = false) where T : Component
        {
            if (behaviour.TryGetComponent(out t) && !isAllowOverlap)
            {
                return;
            }

            t = behaviour.gameObject.AddComponent<T>();
        }
        
        public static void TryAddNewObject<T>(Behaviour behaviour, out T t) where T : Component
        {
            var obj = new GameObject(typeof(T).Name);
            
            obj.transform.SetParent(behaviour.transform);
            
            t = obj.gameObject.AddComponent<T>();
        }
    }
}
