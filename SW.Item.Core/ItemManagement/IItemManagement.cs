using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using SW.Item.Data.Common;
using SW.Item.Data.Entities;
using SW.Item.Data.Models;

namespace SW.Item.Core.ItemManagement
{
    public interface IItemManagement
    {
        Response BatchAddItem();
        Response AddItem(Data.Entities.Item item);
        Response EditItem(Data.Entities.Item item);
        Response DeleteItemImg(int itemId, string img, string uploadPath);
        Response UploadItemImages(IFormFile[] images, string uploadPath);
        Response DeleteItemByItemUserId(int itemId, int userId, string uploadPath);
        Response DeleteItemByItemId(int itemId, string uploadPath);
        ItemModel GetItem(int id);
        ItemModel[] GetItems();
        ItemModel[] GetItemsByCategory(int categoryId);
        ItemModel[] GetItemsByUser(int userId);

        Response AddRemoveLike(ItemLike like);

        Response AddItemExchange(int itemId, List<ItemModel> itemsForExchange);
        Response RemoveItemExchange(int exchangeId);
        Response ItemExchangeStatusUpdate(int exchangeId, int statusId);
    }
}
