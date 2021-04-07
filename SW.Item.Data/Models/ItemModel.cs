using SW.Item.Data.Common.Models;

namespace SW.Item.Data.Models
{
    public class ItemModel
    {
        public Entities.Item Item { get; set; }
        public UserInfo User { get; set; }
    }
}
