using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ILoveYou.Models;
using Microsoft.Ajax.Utilities;
using ILoveYou.Tools;

namespace ILoveYou.Controllers
{
    public class LoveController : Controller
    {
        // GET: Love
        public ActionResult Index()
        {
            LogHelper.WriteLog("----页面加载");
            MessageContext db = new MessageContext();
            //string url = Server.MapPath("/Content/text/Content.txt");
            //List<string> content = System.IO.File.ReadLines(url).ToList();
            //ViewBag.Name = content[0];
            //ViewBag.Titles = content[1];
            //ViewBag.Content = content[2];
            var messages = db.Messages.ToList();

            return View(messages);
        }

        [HttpGet]
        public ActionResult ClickQq()
        {
            string state = new Random(100000).Next(99, 99999).ToString();//随机数
            Session["QQState"] = state;
            string appID = ConfigurationManager.AppSettings["QQAppID"];
            string qqAuthorizeURL = ConfigurationManager.AppSettings["QQAuthorizeURL"];
            string callback = ConfigurationManager.AppSettings["QQCallBack"];
            string authenticationUrl = string.Format("{0}?client_id={1}&response_type=code&redirect_uri={2}&state={3}", qqAuthorizeURL, appID, callback, state);//互联地址
            return JavaScript("window.open('"+authenticationUrl+"','','scrollbars=yes,status =yes')");
        }

        /// <summary>
        /// 保存文本
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveText(Message message)
        {
            //string url = Server.MapPath("/Content/text/Content.txt");
            //System.IO.File.WriteAllLines(url, new string[] { name, title, content });

            MessageContext db = new MessageContext();
            message.CreatedAt = DateTime.Now;
            message.CreatedBy = User.Identity.Name;
            var backImg = Request.Files["BackImg"];
            if (message.BackImg != null&&backImg!=null)
            {
                string file = backImg.FileName;
                FileInfo files = new FileInfo(file);
                var guidImg = Guid.NewGuid();
                string pathStr = "/Upload/BackImg/" + guidImg + files.Extension;
                backImg.SaveAs(Request.MapPath(pathStr));
                message.BackImg = pathStr;
            }
            var backMus = Request.Files["BackMusic"];
            if (message.BackMusic != null && backMus != null)
            {
                string file = backMus.FileName;
                FileInfo files = new FileInfo(file);
                var guidMus = Guid.NewGuid();
                string pathStr = "/Upload/BackMusic/" + guidMus + files.Extension;
                backMus.SaveAs(Request.MapPath(pathStr));
                message.BackMusic = pathStr;
            }

            db.Messages.Add(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}