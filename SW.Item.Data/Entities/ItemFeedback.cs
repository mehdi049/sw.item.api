using System;
using System.Collections.Generic;
using System.Text;

namespace SW.Item.Data.Entities
{
    public class ItemFeedback
    {
        public int Id { get; set; }
        public string Feedback { get; set; }
        public DateTime AddedTime { get; set; }

        public Item Item { get; set; }
        public int ItemId { get; set; }

        public int UserId { get; set; }
    }
}
