using System;
using System.Collections.Generic;
using System.Text;
using SW.Item.Data.Common;

namespace SW.Item.Core.ItemManagement
{
    public interface IItemManagement
    {
        Response BatchAddItem();
        Data.Entities.Item GetItem(int id);
        Data.Entities.Item[] GetItems();
    }
}
