using System;
using System.Collections.Generic;

namespace goedle_sdk.detail
{
	public class GoedleAtom
	{
		/* NOT YET IMPLEMENTED
		 *
		private string dev_version;
		private string screen = null;
		private string uuid = null;
		private string device_type = null;
		*/

		private string app_key;
		private string user_id;
		private int ts;
		//private string locale;
		private string event_name = null;
		private string event_value = null;
		private int timezone = 0;
		private string event_id = null;
		private int build_nr = GoedleConstants.BUILD_NR;
		private string trait_key = null;
		private string trait_value = null;
		private string app_version = null;
		private string anonymous_id = null;
		private string uuid = null;

		public GoedleAtom (string app_key, 
		                   string user_id, 
		                   int ts,
		                   string event_name,
		                   string event_id, 
		                   string event_value,
		                   int timezone, 
		                   string app_version,
		                   bool ga_active,
		                   string anonymous_id,
		                   string trait_key, 
		                   string trait_value

		)
						  //string locale)
		{
			//ALWAYS

			this.app_key = app_key;
			this.user_id = user_id;
			if (this.user_id == null) {
				Console.Write ("Maybe the GoedleAPI.init(); isn`t called in the Application class. Or you don't have set the GoedleAPI.setUserId(userId)?");
			}
			this.ts = ts;
			this.event_name = event_name;
			this.app_version = app_version;
			//this.locale = locale;

			// The Timzone is in seconds and with -1, so we have to transform it
			this.timezone = timezone;
	
			if (!string.IsNullOrEmpty (event_id))
				this.event_id = event_id;
			if (!string.IsNullOrEmpty (event_value))
				this.event_value = event_value;
			if (!string.IsNullOrEmpty (trait_key))
				this.trait_key = trait_key;
			if (!string.IsNullOrEmpty (trait_value))
				this.trait_value = trait_value;

			// For GA support
			if (!string.IsNullOrEmpty (anonymous_id))
				this.anonymous_id = anonymous_id;
			// This is for the google analytics case
			if (ga_active)
				this.uuid = anonymous_id;

		}


		public Dictionary<string, object> getGoedleAtomDictionary ()
		{
			
			Dictionary<string, object> goedleAtom = 
				new Dictionary<string, object> ();
			goedleAtom.Add ("app_key", this.app_key);
			goedleAtom.Add ("user_id", this.user_id);
			goedleAtom.Add ("ts", this.ts);
			goedleAtom.Add ("event", this.event_name);
			goedleAtom.Add ("build_nr", build_nr);
			goedleAtom.Add ("app_version", this.app_version);

			/*if (!string.IsNullOrEmpty (this.locale))
				goedleAtom.Add ("locale", this.locale);*/
			if (!string.IsNullOrEmpty (anonymous_id))
				goedleAtom.Add ("anonymous_id", this.anonymous_id);
			if (!string.IsNullOrEmpty (uuid))
				goedleAtom.Add ("uuid", this.anonymous_id);
			if (this.timezone != Int32.MaxValue)
				goedleAtom.Add ("timezone", this.timezone);
			if (!string.IsNullOrEmpty (event_id))
				goedleAtom.Add ("event_id", this.event_id);
			if (!string.IsNullOrEmpty (event_value))
				goedleAtom.Add ("event_value", this.event_value);
	
			if (!string.IsNullOrEmpty (trait_key) && !string.IsNullOrEmpty (trait_value))
				goedleAtom.Add (this.trait_key, this.trait_value);
			return goedleAtom;
		}
	}
}
