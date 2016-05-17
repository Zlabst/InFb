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
        private string GetTag(string link)
        {
            string result = "";
            int pos = link.IndexOf("?set=a.");
            pos += 7;
            while (link[pos] != '.')
            {
                result += link[pos++];
            }
            /*for (int i = pos + 7; i < pos + 23; i++)//getting tag
            {
                result += link[i];
            }*/
            foreach (char c in result)//veryfing if tag is proper
            {
                if (!Char.IsDigit(c))
                {
                    return null;
                }
            }
            return result;
        }


        public List<string> GetLinks(string link)
        {
            if (link == null)
            {
                return null;
            }
            string strtagName = this.GetTag(link);
            string strAccessToken = "xxx";
            string nextPageUrl = null;
            string imageUrl = null;
            List<string> result = new List<string>();

            WebRequest webRequest = null;
            if (webRequest == null && string.IsNullOrEmpty(nextPageUrl))
                webRequest = HttpWebRequest.Create(String.Format("https://graph.facebook.com/{0}/photos?&fields=id,source&limit=20&access_token={1}", strtagName, strAccessToken));
            else
                webRequest = HttpWebRequest.Create(nextPageUrl);
            try
            {
                var responseStream1 = webRequest.GetResponse().GetResponseStream();
            }
            catch (Exception e)
            {
                return null;
            }

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