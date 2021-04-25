using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SW.Item.Data.Common.Models;

namespace SW.Item.Data.Entities
{
    public class LikedItem
    {
        public int Id { get; set; }

        [NotMapped]
        public UserInfo User { get; set; }
        public int UserId { get; set; }

        public Item Item { get; set; }
        public int ItemId { get; set; }
    }
}
