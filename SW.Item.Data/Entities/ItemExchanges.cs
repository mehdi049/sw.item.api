using System;
using System.Collections.Generic;
using System.Text;

namespace SW.Item.Data.Entities
{
    public class ItemExchanges
    {
        public ItemExchanges()
        {
            ItemsToExchange = new List<Item>();
        }

        public int Id { get; set; }

        public DateTime ExchangeRequestTime { get; set; }

        public DateTime StatusChangeTime { get; set; }

        public int ExchangeStatusId { get; set; }
        public ItemExchangeStatus ExchangeStatus { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public string ItemsToExchangeIds { get; set; }
        public List<Item> ItemsToExchange { get; set; }
    }
}
