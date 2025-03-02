using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Cf.Web
{
    public enum RequestType
    {
        Get,
        Post,
    }

    public static class WebUtil
    {
        #region :: Request

        public static UnityWebRequest CreateRequest(string url, RequestType type = RequestType.Get, object data = null)
        {
            UnityWebRequest request = new UnityWebRequest(url, type.ToString());

            if (data != null)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
                request.uploadHandler = new UploadHandlerRaw(bytes);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            return request;
        }

        public static UnityWebRequest CreateTextureRequest(string url, bool nonReadable = false)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url, nonReadable);

            return request;
        }

        #endregion

        #region :: Convert

        public static Texture2D ConvertTexture(UnityWebRequest request)
        {
            return DownloadHandlerTexture.GetContent(request);
        }

        #endregion
    }
}
