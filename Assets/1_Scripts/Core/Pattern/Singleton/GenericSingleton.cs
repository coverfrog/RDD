using System;
using UnityEngine;

namespace Cf.Pattern
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        private static object _lock = new object();

        private static bool _isRunning = true;
        
        private static T _mInstance;

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (!_isRunning) return null;
                    
                    if (_mInstance != null) return _mInstance;

                    _mInstance = FindAnyObjectByType<T>();
                
                    if (_mInstance != null) return _mInstance;

                    _mInstance = new GameObject(typeof(T).Name).AddComponent<T>();

                    return _mInstance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (_mInstance != null && _mInstance != this)
            {
                Destroy(gameObject);
            }

            else
            {
                _mInstance = this as T;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnApplicationQuit()
        {
            _isRunning = false;
        }
    }
}
