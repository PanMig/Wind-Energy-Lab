using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


namespace goedle_sdk.detail {

    public interface IGoedleDownloadBuffer
    {
        DownloadHandlerBuffer downloadHandlerBuffer { get; }
        string text { get; }
    }

    public class GoedleDownloadBuffer : IGoedleDownloadBuffer
    {

        DownloadHandlerBuffer _downloadHandlerBuffer { get;}

        public GoedleDownloadBuffer()
        {
            _downloadHandlerBuffer = new DownloadHandlerBuffer();
        }

        public DownloadHandlerBuffer downloadHandlerBuffer
        {
            get { return _downloadHandlerBuffer; }
        }

        public string text
        {
            get { return _downloadHandlerBuffer.text; }
        }

    }
}

