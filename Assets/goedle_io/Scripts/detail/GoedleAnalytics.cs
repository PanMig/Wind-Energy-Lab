using System;
using System.Collections.Generic;
using System.Collections;
using SimpleJSON;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;

namespace goedle_sdk.detail
{
    public class GoedleAnalytics
    {
        private string _api_key = null;
        private string _app_key = null;
        private string _user_id = null;
        private string _anonymous_id = null;
        private string _app_version = null;
        private string _ga_tracking_id = null;
        private string _app_name = null;
        private int _cd1;
        private int _cd2;
        private string _cd_event = null;
        public IGoedleHttpClient _gio_http_client { get; set; }
        private GoedleUtils _goedleUtils = new GoedleUtils();

        //private string locale = null;
        //public static JSONNode _strategy = null;
        public JSONNode _strategy = null;


        public GoedleAnalytics(string api_key, string app_key, string user_id, string app_version, string ga_tracking_id, string app_name, int cd1, int cd2, string cd_event, IGoedleHttpClient gio_http_client)//, string locale)
        {
            _api_key = api_key;
            _app_key = app_key;
            _user_id = user_id;
            _app_version = app_version;
            _ga_tracking_id = ga_tracking_id;
            _app_name = app_name;
            _cd1 = cd1;
            _cd2 = cd2;
            _cd_event = cd_event;
            _gio_http_client = gio_http_client;
            //this.locale = GoedleLanguageMapping.GetLanguageCode (locale);
            track_launch();
        }

        public void reset_user_id(string user_id)
        {
            _user_id = user_id;
        }

        public void set_user_id(string user_id)
        {
            _anonymous_id = _user_id;
            _user_id = user_id;
            track(GoedleConstants.IDENTIFY, null, null, false, null, null);
        }

        public void track_launch()
        {
            track(GoedleConstants.EVENT_NAME_INIT, null, null, true, null, null);
        }

        public void track(string event_name, string event_id, string event_value, bool launch, string trait_key, string trait_value)
        {
            bool ga_active = !String.IsNullOrEmpty(_ga_tracking_id);
            string authentication = null;
            string content = null;
            int ts = getTimeStamp();
            // -1 because c# returns -1 for UTC +1 , * 1000 from Seconds to Milliseconds
            int timezone = (int)(((DateTime.UtcNow - DateTime.Now).TotalSeconds) * -1 * 1000);
            GoedleAtom rt = new GoedleAtom(_app_key, _user_id, ts, event_name, event_id, event_value, timezone, _app_version, _anonymous_id, trait_key, trait_value, ga_active);
            if (rt == null)
            {
                Console.Write("Data Object is None, there must be an error in the SDK!");
            }
            else
            {
                content = rt.getGoedleAtomDictionary().ToString();
                authentication = _goedleUtils.encodeToUrlParameter(content, _api_key);
            }

            string url = GoedleConstants.TRACK_URL;
            _gio_http_client.sendPost(url, content, authentication);

            // Sending tp Google Analytics for now we only support the Event tracking
            string type = "event";

            if (ga_active)
                trackGoogleAnalytics(event_name, event_id, event_value, type);
        }

        private string buildGAUrlDataString(Dictionary<string, string> postData)
        {
            StringBuilder sb = new StringBuilder();
            string url = GoedleConstants.GOOGLE_MP_TRACK;

            sb.Append(url + "?");
            bool first = true;
            foreach (var item in postData)
            {
                if (first)
                    first = false;
                else
                    sb.Append('&');
                sb.Append(item.Key);
                sb.Append('=');
                sb.Append(UnityWebRequest.EscapeURL(item.Value.ToString()));
            }
            return sb.ToString();
        }

        public void trackGoogleAnalytics(string event_name, string event_id, string event_value, string type)
        {
            if (string.IsNullOrEmpty(event_name)) throw new ArgumentNullException();
            // the request body we want to send
            var postData = new Dictionary<string, string>
                           {
                               { "v", GoedleConstants.GOOGLE_MP_VERSION.ToString() },
                {"av", _app_version},
                {"an", _app_name},
                { "tid", _ga_tracking_id },
                { "cid", _user_id },
                               { "t", type },
                                { "ec", getSceneName() },
                               { "ea", event_name },
								//{"ul", this.locale},
                           };


            // This is the Event label in Google Analytics

            if (!String.IsNullOrEmpty(event_id))
            {
                postData.Add("el", event_id);
            }
            if (_goedleUtils.IsFloatOrInt(event_value))
            {
                postData.Add("ev", event_value);
            }

            if (!String.IsNullOrEmpty(_anonymous_id))
            {
                postData.Add("uid", _user_id);
                // For mapping after identify
                // Otherwise we will lost the old client id
                postData.Remove("cid");
                postData.Add("cid", _anonymous_id);
            }
            if (_cd_event == "group" && event_name == "group" && _cd1 != 0 && _cd2 != 0 && _cd1 != _cd2)
            {
                postData.Remove("el");
                postData.Remove("ev");
                postData.Add(String.Concat("cd", _cd1), event_id);
                postData.Add(String.Concat("cd", _cd2), event_value);
            }

            _gio_http_client.sendGet(buildGAUrlDataString(postData));
        }

        public JSONNode requestStrategy (float maximum_blocking_time){
            string url = _goedleUtils.getStrategyUrl(_app_key);

            _gio_http_client.requestStrategy(url, this);

            DateTime dt = DateTime.Now + TimeSpan.FromSeconds(maximum_blocking_time);
            do {

            } while (DateTime.Now < dt);

            return _strategy;
        }


        public void track(string event_name)
        {
            track(event_name, null, null, false, null, null);
        }


        public void track(string event_name, string event_id)
        {
            track(event_name, event_id, null, false, null, null);
        }

        public void track(string event_name, int event_id_i)
        {
            string event_id = event_id_i.ToString();

            track(event_name, event_id, null, false, null, null);
        }

        public void track(string event_name, string event_id, string event_value)
        {
            track(event_name, event_id, event_value, false, null, null);

        }

        public void trackGroup(string group_type, string group_member)
        {
            track("group", group_type, group_member, false, null, null);
        }

        public void trackTraits(string trait_key, string trait_value)
        {
            track(GoedleConstants.IDENTIFY, null, null, false, trait_key, trait_value);
        }

		public int getTimeStamp ()
		{
			return (Int32)(DateTime.UtcNow.Subtract (new DateTime (1970, 1, 1))).TotalSeconds;
		}

		public string getSceneName()
		{
			Scene scene = SceneManager.GetActiveScene();
            if (string.IsNullOrEmpty(scene.name))
            {
                return "NoScence";
            }
            else
            {
                return scene.name;
            }
		}

		private enum HitType
        {
            // ReSharper disable InconsistentNaming
            @event,
            @pageview,
            // ReSharper restore InconsistentNaming
        }



	}
}