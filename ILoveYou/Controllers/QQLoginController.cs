using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ILoveYou.Models;
using Newtonsoft.Json;

namespace ILoveYou.Controllers
{
    public class QQLoginController : Controller
    {
        private const string URL = "https://graph.qq.com/user/get_user_info?oauth_consumer_key=100330589&access_token=9DC2F0EE73AA18011DFA7E94F73F348A&openid=DABFF9E3B831FD725520A6FBAD1505D3&format=json";
        public ActionResult Index()
        {
            try
            {
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL);
                if (request != null)
                {
                    string retval = null;
                    init_Request(ref request);
                    using (var response = request.GetResponse())
                    {
                        using (var reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                        {
                            retval = reader.ReadToEnd();
                        }
                    }
                    retval = retval.Replace(@"\t", "");
                    retval = retval.Replace(@"\n", "");
                    var str = JsonConvert.DeserializeObject<QQModel>(retval);
                    return Json(new { reqData = str, opts=true},JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e)
            {
                return null;
            }
            return null;
            //QOpenClient qzone = null;
            //QConnectSDK.Models.User currentUser = null;
            //var verifier = Request.Params["code"];
            //string state1 = Session["requeststate"].ToString();
            //qzone = new QOpenClient(verifier, state1);
            //currentUser = qzone.GetCurrentUser();
            //if (null != currentUser)
            //{
            //    return Content(currentUser.Nickname);
            //}
            //Session["QzoneOauth"] = qzone; return View();
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