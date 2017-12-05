using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
//using System.Web;
using UnityEngine;

namespace goedle_sdk.detail
{
    public class GoogleWrappedHttpClient
    {


    
        public GoogleWrappedHttpClient ()
        {
        }
            
		public void send(Dictionary<string, string> postData)
            {
			string dataString = buildPostDataString (postData);
			if (!String.IsNullOrEmpty(dataString)){
				var postDataString = dataString;
				// Console.WriteLine(postDataString);
			new WWW(postDataString);}
			else{
				Console.WriteLine("Sorry, we are not able to send this event: " + dataString);
			}
            }

		public string buildPostDataString (Dictionary<string, string> postData)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append (GoedleConstants.GOOGLE_MP_TRACK_URL+"?");
			bool first = true;
			foreach(var item in postData)
			{
				if (first)
					first = false;
				else
					sb.Append('&');
				sb.Append(item.Key);
				sb.Append('=');
				sb.Append(WWW.EscapeURL(item.Value.ToString()));
			}
			return sb.ToString();
		}
}
}