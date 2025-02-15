using System;
using UnityEngine;

namespace Cf.Pattern
{
    public abstract class GenericSingleton<T> : MonoBehaviour where T : Component
    {
        // ins
        private static T _mInstance;

        public static T Instance
        {
            get
            {
                if (_mInstance != null) return _mInstance;

                _mInstance = FindAnyObjectByType<T>();
                
                if (_mInstance != null) return _mInstance;

                _mInstance = new GameObject(typeof(T).Name).AddComponent<T>();

                return _mInstance;
            }
        }

        // awake
        protected void Awake()
        {
            if (_mInstance != null)
            {
                Destroy(gameObject);
            }

            else
            {
                _mInstance = this as T;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
