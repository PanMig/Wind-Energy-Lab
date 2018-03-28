using System.Collections;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.Networking;

namespace goedle_sdk.detail
{

    public interface IGoedleHttpClient
    {
        //JSONNode getStrategy(IUnityWebRequests www, string url);
        void sendPost(string url, string content, string authentification);
        void sendGet(string url);
        void requestStrategy(string url, GoedleAnalytics ga);
        void addUnityHTTPClient(IUnityWebRequests www);
        JSONNode Strategy { get; set; }
        IEnumerator getRequest(string url);
        IEnumerator getJSONResponse(string url, GoedleAnalytics ga);
        IEnumerator postJSONRequest(string url, string content, string authentification);
    }

    public interface IUnityWebRequests
    {
        UnityWebRequest SendWebRequest();
        UnityWebRequest Post(string url, string content);
        UnityWebRequest Get(string url, string content);
        int responseCode { get; set; }
        bool isNetworkError { get; set; }
        bool isHttpError { get; set; }
        string url{ get; set; }

    }

    public class GoedleHttpClient: MonoBehaviour, IGoedleHttpClient 
	{
        JSONNode strategy = null;
        
        IUnityWebRequests _www;

        public JSONNode Strategy
        {
            get
              {
                 return strategy;
            }
            set
              {
                    strategy = value;
              }
       }


        public GoedleHttpClient(){
        }

        public void addUnityHTTPClient(IUnityWebRequests www){
            _www = www;
        }

        public void sendGet( string url)
        {
            StartCoroutine(getRequest( url));
        }


        public void requestStrategy(string url, GoedleAnalytics ga)
        {
            StartCoroutine(getJSONResponse(url, ga));
        }

        public void sendPost(string url, string content, string authentification)
        {
            StartCoroutine(postJSONRequest(url, content, authentification));
        }

        public IEnumerator getRequest(string url)
        {
            UnityWebRequest client = _www as UnityWebRequest;

            using (client = new UnityWebRequest(url, "GET"))
            {
                yield return client.SendWebRequest();
                if (client.isNetworkError || client.isHttpError)
                {
                    Debug.Log(client.error);
                }
                else
                {
                    // Show results as text
                    //Debug.Log(client.downloadHandler.text);
                    // Or retrieve results as binary data
                    //byte[] results = client.downloadHandler.data;
                }
            }
        }

        public IEnumerator getJSONResponse(string url, GoedleAnalytics ga)
        {
            

            UnityWebRequest client = _www as UnityWebRequest;
            client = new UnityWebRequest(url, "GET");

            using (client)
            {
                client.downloadHandler = new DownloadHandlerBuffer();

                yield return client.SendWebRequest();


                if (client.isNetworkError || client.isHttpError)
                {
                    Debug.Log(client.error);
                }
                else
                {
                    // Show results as text
                    JSONNode strategy_json;
                    try
                    {
                        strategy_json = JSON.Parse(client.downloadHandler.text);
                    }
                    catch (Exception e)
                    {
                        strategy_json = JSON.Parse("{\"error\": \" " + e.Message + " \"}");
                    }

                    Debug.Log(strategy_json["config"].ToString());
                    ga._strategy = strategy_json;

                    // Or retrieve results as binary data
                    //byte[] results = client.downloadHandler.data;
                }
            }
        }

        public IEnumerator postJSONRequest( string url, string content ,string authentification)
	    {
            UnityWebRequest client = _www as UnityWebRequest;
            client = new UnityWebRequest(url, "POST");
            using (client)
            {
                byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(content);
                client.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
                client.SetRequestHeader("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(authentification))
                    client.SetRequestHeader("Authorization", authentification);
                client.chunkedTransfer = false;
                yield return client.SendWebRequest();
                if (client.isNetworkError || client.isHttpError)
                {
                    Debug.Log(client.error);
                }
                else
                { 
                   //Debug.Log(content);
                }
            }
	    }

	}

}


