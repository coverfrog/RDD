using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public class SimpleHttpClientSample : MonoBehaviour
{
    [Header("Option")]
    [SerializeField] private string mParam = "debug";
    
    [Header("Http")] 
    [SerializeField] private int mHttpPort = 20000;
    [SerializeField] private string mSubPath = "api";
    
    private bool _mRequestRunning;

    private void Awake()
    {
        // check sub path
        if (string.IsNullOrEmpty(mSubPath))
        {
            return;
        }

        if (mSubPath.StartsWith("/"))
        {
            return;
        }

        mSubPath = $"/{mSubPath}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Request();
        }
    }

    private void Request()
    {
        if (_mRequestRunning)
        {
            return;
        }

        StartCoroutine(CoRequest(Debug.Log));
    }

    private IEnumerator CoRequest(Action<string> callback)
    {
        string paramLower = mParam.ToLower();
        string url = $"http://127.0.01:{mHttpPort}{mSubPath}?{paramLower}={paramLower}";

        using UnityWebRequest request = UnityWebRequest.Get(url);
        
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string text = request.downloadHandler.text;
            
            callback?.Invoke(text);
        }
    }
}
