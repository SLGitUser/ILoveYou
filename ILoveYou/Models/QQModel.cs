using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ILoveYou.Models
{
    public class QQModel
    {
        public string openid { get; set; }
        

        public string ret { get; set; } //返回码
        public string msg { get; set; } //如果ret<0，会有相应的错误信息提示，返回数据全部用UTF-8编码。
        public string nickname { get; set; } //用户在QQ空间的昵称。
        public string figureurl { get; set; } //大小为30×30像素的QQ空间头像URL。
        public string figureurl_1 { get; set; } //大小为50×50像素的QQ空间头像URL。
        public string figureurl_2 { get; set; } //大小为100×100像素的QQ空间头像URL。
        public string figureurl_qq_1 { get; set; } //大小为40×40像素的QQ头像URL。
        public string figureurl_qq_2 { get; set; } //大小为100×100像素的QQ头像URL。需要注意，不是所有的用户都拥有QQ的100×100的头像，但40×40像素则是一定会有。
        public string gender { get; set; } //性别。 如果获取不到则默认返回”男”
        public string is_yellow_vip { get; set; } //标识用户是否为黄钻用户（0：不是；1：是）。
        public string vip { get; set; } //标识用户是否为黄钻用户（0：不是；1：是）
        public string yellow_vip_level { get; set; } //黄钻等级
        public string level { get; set; } //黄钻等级
        public string is_yellow_year_vip { get; set; } //标识是否为年费黄钻用户（0：不是； 1：是）



    }
}