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
    class InstGetter
    {
        public List<string> GetLinks(string tag)
        {
            string strtagName = tag;
            string strAccessToken = "iii";
            string nextPageUrl = null;
            string imageUrl = null;
            List<string> result = new List<string>();
            
            WebRequest webRequest = null;
            if (webRequest == null && string.IsNullOrEmpty(nextPageUrl))
                webRequest = HttpWebRequest.Create(String.Format("https://api.instagram.com/v1/tags/{0}/media/recent?access_token={1}", strtagName, strAccessToken));
            else
                webRequest = HttpWebRequest.Create(nextPageUrl);
                
            var responseStream = webRequest.GetResponse().GetResponseStream();
                
            Encoding encode = System.Text.Encoding.Default;
            using (StreamReader reader = new StreamReader(responseStream, encode))
            {
                JToken token = JObject.Parse(reader.ReadToEnd());
                var pagination = token.SelectToken("pagination");
                if (pagination != null && pagination.SelectToken("images") != null)
                {
                    nextPageUrl = pagination.SelectToken("images").ToString();
                }
                else
                {
                    nextPageUrl = null;
                }
                var images = token.SelectToken("data").ToArray();
                foreach (var image in images)
                {
                    imageUrl = image.SelectToken("images").SelectToken("thumbnail").SelectToken("url").ToString();
                    result.Add(imageUrl);
                    if (String.IsNullOrEmpty(imageUrl))
                        Console.WriteLine("broken image URL");

                    //Console.WriteLine(imageUrl);
                }
            }
            return result;
        }
    }
}
