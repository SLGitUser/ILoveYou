namespace ILoveYou.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Message
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "目标者")]
        public string Receiver { get; set; }

        [StringLength(50)]
        [Display(Name = "留言者")]
        public string Sender { get; set; }

        [StringLength(50)]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Display(Name = "背景图片")]
        public string BackImg { get; set; }

        [Display(Name = "背景音乐")]
        public string BackMusic { get; set; }

        [Display(Name = "留言内容")]
        public string Content { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "创建者")]
        [StringLength(50)]
        public string CreatedBy { get; set; }
    }
}
