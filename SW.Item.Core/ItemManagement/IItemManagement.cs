using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using SW.Item.Data.Common;
using SW.Item.Data.Models;

namespace SW.Item.Core.ItemManagement
{
    public interface IItemManagement
    {
        Response BatchAddItem();
        Response AddItem(Data.Entities.Item item);
        Response UploadItemImages(IFormFile[] images, string uploadPath);
        ItemModel GetItem(int id);
        ItemModel[] GetItems();
        ItemModel[] GetItemsByCategory(int categoryId);
    }
}
