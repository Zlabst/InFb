using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InFb.Source
{
    class FbGetter
    {
        public List<string> GetLinks(string tag)
        {
            if (tag == null)
            {
                return null;
            }
            string strtagName = tag;
            string strAccessToken = "xxx";
            string nextPageUrl = null;
            string imageUrl = null;
            List<string> result = new List<string>();

            WebRequest webRequest = null;
            if (webRequest == null && string.IsNullOrEmpty(nextPageUrl))
                webRequest = HttpWebRequest.Create(String.Format("https://graph.facebook.com/{0}/photos?&fields=id,source&limit=20&access_token={1}", strtagName, strAccessToken));
            else
                webRequest = HttpWebRequest.Create(nextPageUrl);

            var responseStream = webRequest.GetResponse().GetResponseStream();

            Encoding encode = System.Text.Encoding.Default;
            using (StreamReader reader = new StreamReader(responseStream, encode))
            {
                JToken token = JObject.Parse(reader.ReadToEnd());
                var pagination = token.SelectToken("data");
                if (pagination != null && pagination.SelectToken("id") != null)
                {
                    nextPageUrl = pagination.SelectToken("id").ToString();
                }
                else
                {
                    nextPageUrl = null;
                }
                var images = token.SelectToken("data").ToArray();
                foreach (var image in images)
                {
                    imageUrl = image.SelectToken("source").ToString();
                    result.Add(imageUrl);
                    if (String.IsNullOrEmpty(imageUrl))
                        Console.WriteLine("broken image URL");
                }
            }
            return result;
        }
    }
}