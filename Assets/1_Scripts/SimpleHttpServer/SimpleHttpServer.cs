using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

public class SimpleHttpServer : MonoBehaviour
{
    [Header("Option")] 
    [SerializeField] private bool mAutoStart;
    
    [Header("Http")] 
    [SerializeField] private int mHttpPort = 20000;
    [SerializeField] private string mSubPath = "/api";

    [Header("Info")] 
    [SerializeField] private SimpleHttpServerStructSample simpleData;
    
    private HttpListener _mHttpListener;
    private Thread _mHttpThread;

    private bool _mIsRunning;

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

    private void Start()
    {
        if (!mAutoStart)
        {
            return;
        }
        
        Run();
    }

    private void Run()
    {
        if (_mIsRunning)
        {
            return;
        }

        // listener start
        _mHttpListener = new HttpListener();
        _mHttpListener.Prefixes.Add($"http://*:{mHttpPort}/");
        _mHttpListener.Start();

        _mIsRunning = true;
        
        // thread start
        _mHttpThread = new Thread(ListenThread);
        _mHttpThread.Start();
        
        // log
        Debug.Log($"[Simple Http] Server Running. Port : {mHttpPort}");
    }

    private void ListenThread()
    {
        while (_mIsRunning)
        {
            try
            {
                // context
                HttpListenerContext context = _mHttpListener.GetContext();

                // to thread pool
                ThreadPool.QueueUserWorkItem(_ => ListenThreadPool(context));
            }
            catch (HttpListenerException)
            {

            }
        }
    }

    private void ListenThreadPool(HttpListenerContext context)
    {
        // client send path
        string path = context.Request.Url.AbsolutePath;
        
        // define 
        string responseString = "";

        // check path
        if (path.StartsWith(mSubPath))
        {
            NameValueCollection query = context.Request.QueryString;
            
            foreach (string key in query.AllKeys)
            {
                string value = query[key];

                responseString += $"{value} = {value}\n";
            }
        }
        
        else
        {
            responseString = "404 Not Found";
        }

        byte[] buffer = Encoding.UTF8.GetBytes(responseString);

        context.Response.ContentType = "application/json";
        context.Response.ContentLength64 = buffer.Length;
        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        context.Response.OutputStream.Close();
    }

    private bool Stop()
    {
        bool isClose = _mHttpListener != null || _mHttpThread != null;
        
        _mIsRunning = false;
        _mHttpListener?.Stop();
        _mHttpThread?.Abort();

        return isClose;
    }

    private void OnApplicationQuit()
    {
        // stop
        if (!Stop())
        {
            return;
        }

        // log
        Debug.Log($"[Simple Http] Server Close. Port : {mHttpPort}");
    }
}
