using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Cf.Scenes
{
    public class SceneLoadToAddressable : MonoBehaviour
    {
        private AsyncOperationHandle<SceneInstance>? _mLoadHandle;

        private bool _mIsLoading;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _mLoadHandle = Addressables.LoadSceneAsync("Assets/0_Scenes/Rdd_1_MainMenu", LoadSceneMode.Single);
            _mIsLoading = true;
            
            if (_mLoadHandle == null)
            {
                return;
            }

            _mLoadHandle.Value.Destroyed += OnDestroyed;
            _mLoadHandle.Value.CompletedTypeless += OnCompletedTypeless;
            _mLoadHandle.Value.Completed += OnCompleted;
        }

        private void Update()
        {
            if (!_mLoadHandle.HasValue || !_mIsLoading)
            {
                return;
            }
            
            PrintPercent(_mLoadHandle.Value.PercentComplete);
        }

        // * call step
        
        // * success
        //  1. OnCompletedTypeless
        //  2. OnCompleted
        
        // * fail
        //  1. OnDestroyed
        
        private void OnDestroyed(AsyncOperationHandle handle)
        {
            _mIsLoading = false;
            _mLoadHandle = null;
        }

        private void OnCompletedTypeless(AsyncOperationHandle handle)
        {
            
        }

        private void OnCompleted(AsyncOperationHandle<SceneInstance> handle)
        {
            _mIsLoading = false;
            _mLoadHandle = null;

            PrintPercent(1.0f);
        }
        
        // * percent

        private void PrintPercent(float percent)
        {
            Debug.Log($"Loading Progress: {percent * 100.0f}%");
        }
    }
}
