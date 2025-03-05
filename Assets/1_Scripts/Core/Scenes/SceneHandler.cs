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

        private static SceneAddressableLoader SceneAddressableLoader { get; set; }

        protected static SceneAddressableLoader AddressableLoad(
                string path, 
                Action<AsyncOperationHandle> actDestroyed,
                Action<AsyncOperationHandle> actCompletedTypeless,
                Action<AsyncOperationHandle<SceneInstance>> actCompleted,
                LoadSceneMode mode = LoadSceneMode.Single,
                bool activeOnLoad = true)
        {
            if (SceneAddressableLoader != null)
            {
                Debug.LogError($"Scene Addressable Loader Is Running");
                return null;
            }

            actDestroyed         += OnAddressableLoadDestroyed;
            actCompletedTypeless += OnAddressableLoadCompletedTypeless;
            actCompleted         += OnAddressableLoadCompleted;
            
            SceneAddressableLoader = new SceneAddressableLoader(
                path,
                actDestroyed,
                actCompletedTypeless,
                actCompleted,
                mode,
                activeOnLoad);

            return SceneAddressableLoader;
        }

        private static void OnAddressableLoadDestroyed(AsyncOperationHandle handle)
        {
            SceneAddressableLoader.Release();
        }
        
        private static void OnAddressableLoadCompletedTypeless(AsyncOperationHandle handle)
        {
            
        }

        private static void OnAddressableLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
        {
            SceneAddressableLoader.Release();
        }

        #endregion

        #region :: Scene Ctrl

        protected static void SceneActive(AsyncOperationHandle<SceneInstance> handle)
        {
            handle.Result.ActivateAsync().allowSceneActivation = true;
        }

        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); 
#endif
        }

        #endregion
        
        protected virtual IEnumerator Start()
        {
            yield break;
        }
    }
}
