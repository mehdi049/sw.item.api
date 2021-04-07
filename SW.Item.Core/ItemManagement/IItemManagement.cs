using System;
using System.Collections.Generic;
using System.Text;
using SW.Item.Data.Common;
using SW.Item.Data.Models;

namespace SW.Item.Core.ItemManagement
{
    public interface IItemManagement
    {
        Response BatchAddItem();
        ItemModel GetItem(int id);
        ItemModel[] GetItems();
        ItemModel[] GetItemsByCategory(int categoryId);
    }
}
