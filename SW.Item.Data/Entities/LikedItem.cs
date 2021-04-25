using System;
using System.Collections.Generic;
using System.Text;

namespace SW.Item.Data.Entities
{
    public class LikedItem
    {
        public int Id { get; set; }
        public int ItemId { get; }
        public int UserId { get; }
    }
}
