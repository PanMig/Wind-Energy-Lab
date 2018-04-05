using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Runtime.CompilerServices;

namespace goedle_sdk.detail {

    public interface IGoedleWebRequest : IDisposable
    {

        bool isNetworkError { get; }
        bool isHttpError { get; }
        string url { get; set; }
        long responseCode { get; }
        string method { get; set; }
        bool chunkedTransfer{ get; set; }
        DownloadHandlerBuffer downloadHandler { get; set; }
        UploadHandler uploadHandler { get; set; }
        void SetRequestHeader(string name, string value);
        UnityWebRequestAsyncOperation SendWebRequest();

    }

    public class GoedleWebRequest : UnityWebRequest,  IGoedleWebRequest{

        public new extern bool isNetworkError
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        public new extern bool isHttpError
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        public new DownloadHandlerBuffer downloadHandler { get;  set; }

        public new string url {  get; set; }

        public new string method { get; set; }

        public new extern long responseCode
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        public new UploadHandler uploadHandler { get;  set; }

    }

}

