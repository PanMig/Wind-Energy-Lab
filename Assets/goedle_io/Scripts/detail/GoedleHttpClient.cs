using System.Collections;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.Networking;
namespace goedle_sdk.detail
{
    public class GoedleHttpClient: MonoBehaviour 
	{
        public void sendGet( string url, UnityWebRequest www)
        {
            StartCoroutine(getRequest( url, www));
        }

        public void requestStrategy(string url, GoedleAnalytics ga, IGoedleWebRequest gwr, IGoedleDownloadBuffer gdb){
            StartCoroutine(getJSONResponse(url, ga, gwr, gdb));
        }

        /*
        public JSONNode getStrategy(IUnityWebRequests www, string url)
        {
            CoroutineWithData cd = new CoroutineWithData(this, getJSONRequest(www, url));
            yield return cd.coroutine;
            Debug.Log("result is " + cd.result);  //  'success' or 'fail'
            yield return cd.result;
            // TODO RETURN JSON from REQUEST
            //yield return StartCoroutine(getJSONRequest(www, url));
        }
        */
        public void sendPost(string url, string authentification, IGoedleWebRequest gwr, IGoedleUploadHandler guh)
        {
            StartCoroutine(postJSONRequest(url, authentification, gwr, guh));
        }

        public IEnumerator getRequest(string url, UnityWebRequest www)
        {
            www.url = url;
            www.method = "GET";
            using (www)
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    //Show results as text
                    //Debug.Log(client.downloadHandler.text);
                    //Or retrieve results as binary data
                    //byte[] results = client.downloadHandler.data;
                }
            }
        }

        /*
         Returns an JSONNode object this can be accessed via:
         CoroutineWithData cd = new CoroutineWithData(this, LoadSomeStuff( ) );
         yield return cd.coroutine;
         Debug.Log("result is " + cd.result);  //  'JSONNode'
         CoroutineWithData is in GoedleUtils
         */
        public IEnumerator getJSONResponse(string url, GoedleAnalytics ga, IGoedleWebRequest gwr, IGoedleDownloadBuffer gdb)
        {

            using (gwr)
            {
                gwr.url = url;
                gwr.method = "GET";
                gwr.downloadHandler = gdb.downloadHandlerBuffer;
                yield return gwr.SendWebRequest();

                JSONNode strategy_json = null;
                if (gwr.isNetworkError || gwr.isHttpError)
                {
                    strategy_json = JSON.Parse("{\"error\": {  \"isHttpError\": \""+ gwr.isHttpError +"\",  \"isNetworkError\": \"" + gwr.isNetworkError + "\" } }");
                }
                else
                {
                    // Show results as text
                    try
                    {
                        strategy_json = JSON.Parse(gdb.text);
                    }
                    catch (Exception e)
                    {
                        strategy_json = JSON.Parse("{\"error\": \" " + e.Message + " \"}");
                    }
                    // Or retrieve results as binary data
                    //byte[] results = client.downloadHandler.data;
                }
                ga.strategy = strategy_json;
            }
        }

        public IEnumerator postJSONRequest( string url, string authentification, IGoedleWebRequest gwr, IGoedleUploadHandler guh)
	    {
            using (gwr)
            {
                gwr.method = "POST";
                gwr.url = url;
                gwr.uploadHandler = guh.uploadHandler;
                gwr.SetRequestHeader("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(authentification))
                    gwr.SetRequestHeader("Authorization", authentification);
                gwr.chunkedTransfer = false;
                yield return gwr.SendWebRequest();
                if (gwr.isNetworkError || gwr.isHttpError)
                {
                    Debug.Log("{\"error\": {  \"isHttpError\": \"" + gwr.isHttpError + "\",  \"isNetworkError\": \"" + gwr.isNetworkError + "\" } }");
                }
            }
	    }
	}
}


