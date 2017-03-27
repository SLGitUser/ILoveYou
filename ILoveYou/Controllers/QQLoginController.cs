using System.Configuration;
using System.Web.Mvc;
using ILoveYou.Models;
using Newtonsoft.Json;
using ILoveYou.Tools;
using System;
using System.Linq;

namespace ILoveYou.Controllers
{
    public class QQLoginController : Controller
    {
        
        private MessageContext db = new MessageContext();
        public ActionResult Index()
        {
            //此code会在10分钟内过期,用于请求获取Access Token
            string code =Request["code"];
            string appID = ConfigurationManager.AppSettings["QQAppID"];
            string appKey = ConfigurationManager.AppSettings["QQAppKey"];
            string openID = "";
            if (!string.IsNullOrEmpty(code))
            {
            string qQCallBack= ConfigurationManager.AppSettings["QQCallBack"];
            string redUrl = "https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id="+appID+ "&client_secret="+ appKey+ "&code="+code+ "&redirect_uri="+qQCallBack;

                string result = UserWebResponse.WebResponse(redUrl);
                string token = "";
                if (!string.IsNullOrEmpty(result))
                {
                    int tokenIndex = result.IndexOf("access_token");
                    token = result.Substring(tokenIndex+13, result.IndexOf("&")- result.IndexOf("=")-1);
                    Session["access_token"] = token;
                }
                if (!string.IsNullOrEmpty(token))
                {
                    LogHelper.WriteLog("获取OpenID");
                    redUrl = "https://graph.qq.com/oauth2.0/me?access_token=" + token;
                    result = UserWebResponse.WebResponse(redUrl);
                    result = result.Substring(result.IndexOf('{'), result.IndexOf('}') - result.IndexOf('{') + 1);
                    Session["openId"] = JsonConvert.DeserializeObject<QQModel>(result).openid;
                    openID = Session["openId"].ToString();
                    if (!string.IsNullOrEmpty(appID) && !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(openID))
                    {
                        LogHelper.WriteLog("获取用户信息");
                        redUrl = "https://graph.qq.com/user/get_user_info?access_token=" + token + "&oauth_consumer_key=" + appID + "&openid=" + openID;
                        result = UserWebResponse.WebResponse(redUrl);
                        QQModel userInfo = JsonConvert.DeserializeObject<QQModel>(result);
                        userInfo.openid = openID;
                        LogHelper.WriteLog("保存到数据库");
                        SaveUserDataToDB(userInfo);
                        LogHelper.WriteLog("保存到Session");
                        SaveToSession(userInfo);//将qq用户信息存储在Session中
                        LogHelper.WriteLog("处理数据完毕，执行页面操作！");
                        return Content("<script>window.opener.location.href = window.opener.location.href;window.close();</script>");
                    }
                }
                
                return Content(result);
            }
            return Content("错误！");
        }
        //将QQ用户信息保存到数据库
        public void SaveUserDataToDB(QQModel userInfo)
        {
            int existCount = db.UserInfoes.Count(m=>m.QqOpenId==userInfo.openid);
            LogHelper.WriteLog(string.Format("OpenId:{0},库中包含{1}条相关数据！",userInfo.openid,existCount));
            if (existCount>0)
            {
                return;
            }
            try
            {
                var at = DateTime.Now;
                LogHelper.WriteLog("---准备创建用户数据实例");
                db.UserInfoes.Add(new UserInfo
                {
                    NickName = userInfo.nickname,
                    QqOpenId = userInfo.openid,
                    CreatedAt = at,
                    CreatedBy = "SYSTEM"
                });
                LogHelper.WriteLog("---准备保存用户数据");
                db.SaveChanges();
                LogHelper.WriteLog("---保存成功");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("---保存用户数据出错，原因："+ex.Message);
                throw;
            }
        }
        //将用户昵称和头像url保存到Session
        public void SaveToSession(QQModel userInfo)
        {
            Session["UserNickName"] = userInfo.nickname;
            Session["UserFigureurl"] = userInfo.figureurl_qq_1;
        }

    }
}