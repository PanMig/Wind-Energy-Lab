using System;
using System.Collections.Generic;
using SimpleJSON;

namespace goedle_sdk.detail
{
	public class GoedleAtom
	{
		/* NOT YET IMPLEMENTED
		 *
		private string dev_version;
		private string screen = null;
		private string device_type = null;
		private string locale;
		*/

        private string _app_key;
        private string _user_id;
        private int _ts;
        private string _event_name = null;
        private string _event_value = null;
        private int _timezone;
        private string _event_id = null;
        private string _build_nr = GoedleConstants.BUILD_NR;
        private string _trait_key = null;
        private string _trait_value = null;
        private string _app_version = null;
        private string _anonymous_id = null;
        private string _uuid = null;

		public GoedleAtom (string app_key, 
		                  string user_id, 
						  int ts, 
		                  string event_name, 
		                  string event_id, 
		                  string event_value,
		                  int timezone,
		                  string app_version,
						  string anonymous_id,
		                  string trait_key, 
						  string trait_value,
						  bool ga_active)
		{
            _app_key = app_key;
			if (user_id == null) {
				Console.Write ("Maybe the GoedleAPI.init(); isn`t called in the Application class. Or you don't have set the GoedleAPI.setUserId(userId)?");
			}else
			{
                _user_id = user_id;
			}			
            _ts = ts;
            _event_name = event_name;
			if (!string.IsNullOrEmpty (event_id))
                _event_id = event_id;
			if (!string.IsNullOrEmpty (event_value))
                _event_value = event_value;
			if (!string.IsNullOrEmpty (trait_key))
                _trait_key = trait_key;
			if (!string.IsNullOrEmpty (trait_value))
                _trait_value = trait_value;
			if (!string.IsNullOrEmpty (anonymous_id))
                _anonymous_id = anonymous_id;
				// This is for the google analytics case
				if (ga_active)	
                    _uuid = anonymous_id;
            _timezone = timezone;
            _app_version = app_version;

		}

        public JSONObject getGoedleAtomDictionary ()
		{
            JSONObject goedleAtom = new JSONObject();
            goedleAtom.Add ("app_key", _app_key);
            goedleAtom.Add ("user_id", _user_id);
            goedleAtom.Add ("ts", _ts);
            goedleAtom.Add ("event", _event_name);
            goedleAtom.Add ("build_nr", _build_nr);
            goedleAtom.Add ("app_version", _app_version);
            goedleAtom.Add ("timezone", _timezone);
			/*if (!string.IsNullOrEmpty (this.locale))
				goedleAtom.Add ("locale", this.locale);*/
            if (!string.IsNullOrEmpty (_anonymous_id))
                goedleAtom.Add ("anonymous_id", _anonymous_id);
            if (!string.IsNullOrEmpty (_uuid))
                goedleAtom.Add ("uuid", _uuid);
            if (!string.IsNullOrEmpty (_event_id))
                goedleAtom.Add ("event_id", _event_id);
            if (!string.IsNullOrEmpty (_event_value))
                goedleAtom.Add ("event_value", _event_value);
	
            if (!string.IsNullOrEmpty (_trait_key) && !string.IsNullOrEmpty (_trait_value))
                goedleAtom.Add (_trait_key, _trait_value);
			return goedleAtom;
		}
	}
}
