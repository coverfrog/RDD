using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Cf.Scenes
{
    public abstract class SceneHandler : MonoBehaviour
    {
        #region :: Addressable Load

        private static readonly List<SceneAddressableLoader> AddressableLoaderList = new List<SceneAddressableLoader>(2);

        protected static SceneAddressableLoader AddressableLoad(
                string path, 
                Action<AsyncOperationHandle> actDestroyed,
                Action<AsyncOperationHandle> actCompletedTypeless,
                Action<AsyncOperationHandle<SceneInstance>> actCompleted,
                LoadSceneMode mode = LoadSceneMode.Single,
                bool activeOnLoad = true)
        {
            actDestroyed         += OnAddressableLoadDestroyed;
            actCompletedTypeless += OnAddressableLoadCompletedTypeless;
            actCompleted         += OnAddressableLoadCompleted;
            
            SceneAddressableLoader loader = new SceneAddressableLoader(
                path,
                actDestroyed,
                actCompletedTypeless,
                actCompleted,
                mode,
                activeOnLoad);

            AddressableLoaderList.Add(loader);
            
            return loader;
        }

        private static void OnAddressableLoadDestroyed(AsyncOperationHandle handle)
        {
            
        }
        
        private static void OnAddressableLoadCompletedTypeless(AsyncOperationHandle handle)
        {
            
        }

        private static void OnAddressableLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
        {
            
        }
        
        protected static void SceneActive(AsyncOperationHandle<SceneInstance> handle)
        {
            handle.Result.ActivateAsync().allowSceneActivation = true;
        }

        #endregion


        protected virtual IEnumerator Start()
        {
            yield break;
        }
    }
}
