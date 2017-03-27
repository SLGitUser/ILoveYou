using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ILoveYou.Tools
{
    public static class UserWebResponse
    {
        public static string WebResponse(string url)
        {
            try
            {
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                if (request != null)
                {
                    string retval = null;
                    init_Request(ref request);
                    using (var response = request.GetResponse())
                    {
                        using (var reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                        {
                            retval = reader.ReadToEnd();
                            return retval;
                        }
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }

        private static void init_Request(ref System.Net.HttpWebRequest request)
        {
            //request.Accept = "text/json,*/*;q=0.5";
            request.Proxy = null;
            request.KeepAlive = false;
            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "*/*";
            request.Headers.Add("Accept-Charset", "utf-8;q=0.7,*;q=0.7");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            request.AutomaticDecompression = System.Net.DecompressionMethods.GZip;
            request.Timeout = 8000;
        }
    }
}