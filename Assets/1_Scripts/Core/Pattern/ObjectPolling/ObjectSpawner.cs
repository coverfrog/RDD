using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Cf.Pattern
{
    public abstract class ObjectSpawner<T> : MonoBehaviour where T : Behaviour
    {
        [Header("Option")] 
        [SerializeField] [Min(50)] private int mDefaultCapacity = 50;
        [SerializeField] [Min(100)] private int mmSpawnMaxCount = 10000;
        
        [Header("Reference")]
        [SerializeField] private T mPrefab;
        
        private ObjectPool<T> _mObjPool;

        protected void Start()
        {
            _mObjPool = new ObjectPool<T>(CreateFunc, OnActionOnGet, OnActionOnRelease, OnActionOnDestroy, true, mDefaultCapacity, mmSpawnMaxCount);
        }


        protected abstract void OnCreateFunc(T obj);
        
        protected abstract void OnActionOnGet(T obj);

        protected abstract void OnActionOnRelease(T obj);

        protected abstract void OnActionOnDestroy(T obj);


        private T CreateFunc()
        {
            T t = Instantiate(mPrefab);

            if (t.TryGetComponent(out IObjectPoolAble<T> objectPoolAble))
            {
                objectPoolAble.SetPool(_mObjPool);
            }

            OnCreateFunc(t);
            
            return t;
        }

        private void ActionOnGet(T obj)
        {
            OnActionOnGet(obj);
        }

        private void ActionOnRelease(T obj)
        {
            obj.gameObject.SetActive(false);

            OnActionOnRelease(obj);
        }

        private void ActionOnDestroy(T obj)
        {
            Destroy(obj.gameObject);

            OnActionOnDestroy(obj);
        }
    }
}
