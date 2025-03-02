using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Cf.Scenes
{
    public class SceneAddressableLoader
    {
        private event Action<AsyncOperationHandle> OnActDestroyed;
        private event Action<AsyncOperationHandle> OnActCompletedTypeless;
        private event Action<AsyncOperationHandle<SceneInstance>> OnActCompleted;
        
        private AsyncOperationHandle<SceneInstance>? _mLoadHandle;
        private AsyncOperationHandle<SceneInstance>? _nSceneHandle;
        
        public SceneAddressableLoader(
            string path, 
            Action<AsyncOperationHandle> actDestroyed,
            Action<AsyncOperationHandle> actCompletedTypeless,
            Action<AsyncOperationHandle<SceneInstance>> actCompleted,
            LoadSceneMode mode = LoadSceneMode.Single,
            bool activeOnLoad = true)
        {
            Path = path;

            OnActDestroyed = actDestroyed;
            OnActCompletedTypeless = actCompletedTypeless;
            OnActCompleted = actCompleted;
            
            _mLoadHandle = Addressables.LoadSceneAsync(path, mode, activeOnLoad);
            _mLoadHandle.Value.Destroyed += OnDestroyed;
            _mLoadHandle.Value.CompletedTypeless += OnCompletedTypeless;
            _mLoadHandle.Value.Completed += OnCompleted;
        }

        public string Path { get; }

        
        public float Percent => _mLoadHandle.HasValue ? _mLoadHandle.Value.PercentComplete * 100.0f : 0.0f;

        
        private void OnDestroyed(AsyncOperationHandle handle)
        {
            _mLoadHandle = null;
            
            OnActDestroyed?.Invoke(handle);
        }

        private void OnCompletedTypeless(AsyncOperationHandle handle)
        {
            OnActCompletedTypeless?.Invoke(handle);
        }

        private void OnCompleted(AsyncOperationHandle<SceneInstance> handle)
        {
            _mLoadHandle = null;
            _nSceneHandle = handle;
            
            OnActCompleted?.Invoke(handle);
        }

        public AsyncOperation Active()
        {
            if (!_nSceneHandle.HasValue)
            {
                return null;
            }

            AsyncOperation op = _nSceneHandle.Value.Result.ActivateAsync();

            op.allowSceneActivation = true;

            return op;
        }
    }
}
