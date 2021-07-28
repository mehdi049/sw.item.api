using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SW.Item.Data.Models;

namespace SW.Item.Data.Entities
{
    public class ItemExchanges
    {
        public ItemExchanges()
        {
            ItemsToExchange = new List<ItemModel>();
        }

        public int Id { get; set; }

        public DateTime ExchangeRequestTime { get; set; }

        public DateTime StatusChangeTime { get; set; }

        public int ExchangeStatusId { get; set; }
        public ItemExchangeStatus ExchangeStatus { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public string ItemsToExchangeIds { get; set; }

        [NotMapped]
        public List<ItemModel> ItemsToExchange { get; set; }
    }
}
