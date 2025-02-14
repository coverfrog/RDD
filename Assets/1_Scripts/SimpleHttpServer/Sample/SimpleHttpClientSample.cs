using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public class SimpleHttpClientSample : MonoBehaviour
{
    [Header("Option")] 
    [SerializeField] private bool mUseLog;
    
    [Header("Http")] 
    [SerializeField] private int mHttpPort = 20000;
    [SerializeField] private string mSubPath = "/api";
    
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
            Get("testStr");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Get("testInt");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Get("testFloat");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Get("testBoolean");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Get("not");
        }
    }

    private void Get(string str)
    {
        if (_mRequestRunning)
        {
            return;
        }

        StartCoroutine(CoRequest(str, (msg)  =>
        {
            if (mUseLog)
            {
                Debug.Log(msg);
            }
        }));
    }

    private IEnumerator CoRequest(string param, Action<string> callback)
    {
        string url = $"http://127.0.01:{mHttpPort}{mSubPath}?{param}={param}";

        using UnityWebRequest request = UnityWebRequest.Get(url);
        
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string text = request.downloadHandler.text;
            
            callback?.Invoke(text);
        }
    }
}
