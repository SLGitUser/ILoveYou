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
        public string Receiver { get; set; }

        [StringLength(50)]
        public string Sender { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public string BackImg { get; set; }

        public string BackMusic { get; set; }

        public string Content { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
    }
}
