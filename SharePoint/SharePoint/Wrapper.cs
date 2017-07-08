using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Json;
using System.Threading.Tasks;

namespace SharePoint
{
    class Wrapper
    {
        private static Wrapper instance;

        private Wrapper() { }

        public static Wrapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Wrapper();
                }
                return instance;
            }
        }

        public async Task<bool> TryToLogin(string username, string password)
        {
            // Create an HTTP web request using the URL:
            string url = "http://tojejedno.scrilab.sk/mina/SharePoint/api/login.php?username="+username+"&password="+password;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    if (jsonDoc.ContainsKey("result"))
                    {
                        string res = jsonDoc["result"];
                        if (res.Equals("OK"))
                        {
                            return true;
                        }
                    }
                    return false;

                    // Return the JSON document:
                }
                return false;
            }
            return false;
        }
    }
}
