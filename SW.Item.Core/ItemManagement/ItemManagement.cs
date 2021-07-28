using SW.Item.Data.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using SW.Item.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SW.Item.Data.Common.Helpers;
using SW.Item.Data.Common.Models;
using SW.Item.Data.Entities;
using SW.Item.Data.Models;
using Microsoft.AspNetCore.Http;

namespace SW.Item.Core.ItemManagement
{
    public class ItemManagement : IItemManagement
    {
        private ApplicationDbContext _dbContext;


        public ItemManagement(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Response BatchAddItem()
        {
            try
            {
                /** insert conditions **/
                if (_dbContext.ItemCondition.Find(1) == null)
                    _dbContext.ItemCondition.Add(new ItemCondition()
                    {
                        Condition = "Occasion"
                    });
                if (_dbContext.ItemCondition.Find(2) == null)
                    _dbContext.ItemCondition.Add(new ItemCondition()
                    {
                        Condition = "Neuf"
                    });
                _dbContext.SaveChanges();

                /** insert item status **/
                if (_dbContext.ItemStatus.Find(1) == null)
                    _dbContext.ItemStatus.Add(new ItemStatus()
                    {
                        Status = "Accepté"
                    });
                if (_dbContext.ItemStatus.Find(2) == null)
                    _dbContext.ItemStatus.Add(new ItemStatus()
                    {
                        Status = "Rejeté"
                    });
                if (_dbContext.ItemStatus.Find(3) == null)
                    _dbContext.ItemStatus.Add(new ItemStatus()
                    {
                        Status = "En cours"
                    });
                _dbContext.SaveChanges();

                /** insert items **/

                string[] images =
                {
                    "img1.jpg", "img2.jpg", "img3.jpg", "img4.jpg", "img5.jpg", "img6.jpg", "img7.jpg", "img8.jpg",
                    "img9.jpg", "img10.jpg"
                };
                Random rnd = new Random();
                List<Data.Entities.Item> items = new List<Data.Entities.Item>();
                for (int i = 0; i < 1000; i++)
                {
                    string img = "";
                    for (int j = 0; j < rnd.Next(1, 5); j++)
                        img += images[rnd.Next(0, 10)] + ";";

                    Data.Entities.Item item = new Data.Entities.Item()
                    {
                        Title = RandomString(20),
                        Description = RandomString(350),
                        Price = rnd.Next(1, 2000),
                        SubCategoryId = rnd.Next(89, 126),
                        ConditionId = rnd.Next(1, 2),
                        Exchange = rnd.Next(1, 2000) % 2 == 0,
                        AddedTime = RandomDay(),
                        ItemStatusId = rnd.Next(1, 3),
                        UserId = rnd.Next(1, 6),
                        Images = img
                    };
                    if (item.Exchange)
                    {
                        item.ExchangeWithCategoryId = rnd.Next(1, 7);
                        item.ExchangeWithSubCategoryId = rnd.Next(1, 3);
                    }
                    items.Add(item);
                }

                items.ForEach(l => _dbContext.Entry(l).State = EntityState.Added);
                _dbContext.Item.AddRange(items);
                _dbContext.SaveChanges();


                /** add item feedback **/
                List<ItemFeedback> itemFeedbacks = new List<ItemFeedback>();
                for (int i = 0; i < 1000; i++)
                {
                    string img = "";
                    for (int j = 0; j < rnd.Next(1, 5); j++)
                        img += images[rnd.Next(0, 10)] + ";";

                    ItemFeedback itemFeedback = new ItemFeedback()
                    {
                        AddedTime = DateTime.Now,
                        UserId = rnd.Next(1, 6),
                        Feedback = RandomString(350),
                        ItemId = rnd.Next(1, 1000)
                    };
                    itemFeedbacks.Add(itemFeedback);
                }

                itemFeedbacks.ForEach(l => _dbContext.Entry(l).State = EntityState.Added);
                _dbContext.ItemFeedback.AddRange(itemFeedbacks);
                _dbContext.SaveChanges();

                return new Response
                {
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }
        }

        public ItemModel GetItem(int id)
        {
            Data.Entities.Item item = _dbContext.Item.Include(x => x.SubCategory)
                .Include(x => x.SubCategory.Category)
                .Include(x => x.Condition)
                .Include(x => x.ItemStatus)
                .Include(x => x.ItemFeedbacks).
                Include(x => x.Likes)
                .Where(x => x.Id == id).FirstOrDefault();

            if (item == null)
                return null;

            Response res = ApiCall
                .ApiGetObject("https://localhost:44363/", "api/user/getAllUsers/");

            if (res.Status == HttpStatusCode.OK)
            {
                UserInfo[] users = JsonConvert.DeserializeObject<UserInfo[]>(res.Body.ToString());

                if (item.ExchangeWithCategoryId.HasValue)
                    item.ExchangeWithCategory = _dbContext.Category.Find(item.ExchangeWithCategoryId);

                if (item.ExchangeWithSubCategoryId.HasValue)
                    item.ExchangeWithSubCategory = _dbContext.SubCategory.Find(item.ExchangeWithSubCategoryId);

                foreach (var feedback in item.ItemFeedbacks)
                    feedback.User = users.Where(x => x.Id == feedback.UserId).FirstOrDefault();

                foreach (var like in item.Likes)
                    like.User = users.Where(x => x.Id == like.UserId).FirstOrDefault();

                List<ItemExchanges> itemExchanges = _dbContext.ItemExchanges.Where(x => x.ItemId == id).ToList();
                if (itemExchanges.Count > 0)
                {
                    foreach (var itemExchange in itemExchanges)
                    {
                        string[] ItemsToExchangeIds = itemExchange.ItemsToExchangeIds.Split(";");
                        foreach (var ItemsToExchangeId in ItemsToExchangeIds)
                            if (!string.IsNullOrEmpty(ItemsToExchangeId))
                            {
                                Data.Entities.Item IE = _dbContext.Item.Include(x => x.SubCategory)
                        .Include(x => x.SubCategory.Category)
                        .Include(x => x.Condition)
                        .Include(x => x.ItemStatus)
                        .Include(x => x.ItemFeedbacks)
                        .Include(x => x.Likes)
                        .Where(x => x.Id == int.Parse(ItemsToExchangeId)).FirstOrDefault();
                                if (IE != null)
                                    itemExchange.ItemsToExchange.Add(new ItemModel()
                                    {
                                        Item = IE,
                                        User = users.Where(x => x.Id == IE.UserId).FirstOrDefault()
                                    });
                            }
                    }
                    item.ItemExchanges = itemExchanges;
                }

                return new ItemModel()
                {
                    Item = item,
                    User = users.Where(x => x.Id == item.UserId).FirstOrDefault()
                };
            }
            return null;
        }

        public ItemModel[] GetItems()
        {
            Response res = ApiCall
                .ApiGetObject("https://localhost:44363/", "api/user/getAllUsers/");

            if (res.Status == HttpStatusCode.OK)
            {
                Data.Entities.Item[] items = _dbContext.Item.Include(x => x.SubCategory)
                    .Include(x => x.SubCategory.Category)
                    .Include(x => x.Condition)
                    .Include(x => x.ItemStatus)
                    .Include(x => x.ItemFeedbacks)
                    .OrderByDescending(x => x.AddedTime)
                    .Where(x => x.ExchangeWithCategoryId.HasValue)
                    .OrderByDescending(x => x.AddedTime)
                    .ToArray();

                UserInfo[] users = JsonConvert.DeserializeObject<UserInfo[]>(res.Body.ToString());

                List<ItemModel> itemModels = new List<ItemModel>();
                foreach (var item in items)
                {
                    if (item.ExchangeWithCategoryId.HasValue)
                        item.ExchangeWithCategory = _dbContext.Category.Find(item.ExchangeWithCategoryId);

                    if (item.ExchangeWithSubCategoryId.HasValue)
                        item.ExchangeWithSubCategory = _dbContext.SubCategory.Find(item.ExchangeWithSubCategoryId);

                    itemModels.Add(new ItemModel()
                    {
                        Item = item,
                        User = users.Where(x => x.Id == item.UserId).FirstOrDefault()
                    });
                }

                return itemModels.ToArray();
            }

            return null;
        }

        public ItemModel[] GetItemsByCategory(int categoryId)
        {
            Response res = ApiCall
                .ApiGetObject("https://localhost:44363/", "api/user/getAllUsers/");

            if (res.Status == HttpStatusCode.OK)
            {
                Data.Entities.Item[] items = _dbContext.Item.Include(x => x.SubCategory)
                    .Include(x => x.SubCategory.Category)
                    .Include(x => x.Condition)
                    .Include(x => x.ItemStatus)
                    .Include(x => x.ItemFeedbacks).OrderByDescending(x => x.AddedTime)
                    .Where(x => x.SubCategory.CategoryId == categoryId)
                    .Where(x => x.ExchangeWithCategoryId.HasValue)
                    .OrderByDescending(x => x.AddedTime)
                    .ToArray();

                UserInfo[] users = JsonConvert.DeserializeObject<UserInfo[]>(res.Body.ToString());

                List<ItemModel> itemModels = new List<ItemModel>();
                foreach (var item in items)
                {
                    if (item.ExchangeWithCategoryId.HasValue)
                        item.ExchangeWithCategory = _dbContext.Category.Find(item.ExchangeWithCategoryId);

                    if (item.ExchangeWithSubCategoryId.HasValue)
                        item.ExchangeWithSubCategory = _dbContext.SubCategory.Find(item.ExchangeWithSubCategoryId);

                    itemModels.Add(new ItemModel()
                    {
                        Item = item,
                        User = users.Where(x => x.Id == item.UserId).FirstOrDefault()
                    });
                }

                return itemModels.ToArray();
            }

            return null;
        }

        public Response AddItem(Data.Entities.Item item)
        {
            try
            {
                item.AddedTime = DateTime.Now;
                item.ItemStatusId = 3;
                _dbContext.Item.Add(item);
                _dbContext.SaveChanges();

                return new Response
                {
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }
        }

        public Response UploadItemImages(IFormFile[] images, string uploadPath)
        {
            try
            {
                var supportedTypes = new[] { "jpg", "jpeg", "png" };
                List<string> itemImages = new List<string>();

                if (images.Length == 0)
                    return new Response()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Veuillez sélectionner une ou plusieurs images pour votre article."
                    };

                foreach (var image in images)
                {
                    string fileExt = Path.GetExtension(image.FileName.ToLower()).Substring(1);
                    if (!supportedTypes.Contains(fileExt))
                        return new Response()
                        {
                            Status = HttpStatusCode.BadRequest,
                            Message = "Extension de l'image est invalide, veuillez utiliser .png, .jpg, .jpeg."
                        };
                    // < 5 mb
                    if (image.Length > 5242880)
                        return new Response()
                        {
                            Status = HttpStatusCode.BadRequest,
                            Message = "L'image téléchargée est très grande, veuillez télécharger une image < 5 Mo."
                        };

                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    string fileName = Guid.NewGuid() + "_" + image.FileName.Replace(" ", "").Replace("(", "").Replace(")", "");
                    using (FileStream fileStream = File.Create(uploadPath + fileName))
                    {
                        image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    itemImages.Add(fileName);
                }

                return new Response { Status = HttpStatusCode.OK, Body = itemImages };

            }
            catch (Exception e)
            {
                return new Response()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite lors d'upload d'image(s), veuillez réessayer."
                };
            }
        }

        public ItemModel[] GetItemsByUser(int userId)
        {
            Response res = ApiCall
                .ApiGetObject("https://localhost:44363/", "api/user/getAllUsers/");

            if (res.Status == HttpStatusCode.OK)
            {
                Data.Entities.Item[] items = _dbContext.Item.Include(x => x.SubCategory)
                    .Include(x => x.SubCategory.Category)
                    .Include(x => x.Condition)
                    .Include(x => x.ItemStatus)
                    .Include(x => x.ItemFeedbacks).OrderByDescending(x => x.AddedTime)
                    .Where(x => x.UserId == userId)
                    .ToArray();

                UserInfo[] users = JsonConvert.DeserializeObject<UserInfo[]>(res.Body.ToString());

                List<ItemModel> itemModels = new List<ItemModel>();
                foreach (var item in items)
                {
                    if (item.ExchangeWithCategoryId.HasValue)
                        item.ExchangeWithCategory = _dbContext.Category.Find(item.ExchangeWithCategoryId);

                    if (item.ExchangeWithSubCategoryId.HasValue)
                        item.ExchangeWithSubCategory = _dbContext.SubCategory.Find(item.ExchangeWithSubCategoryId);

                    List<ItemExchanges> itemExchanges = _dbContext.ItemExchanges.Where(x => x.ItemId == item.Id).ToList();
                    if (itemExchanges.Count > 0)
                    {
                        foreach (var itemExchange in itemExchanges)
                        {
                            string[] ItemsToExchangeIds = itemExchange.ItemsToExchangeIds.Split(";");
                            foreach (var ItemsToExchangeId in ItemsToExchangeIds)
                                if (!string.IsNullOrEmpty(ItemsToExchangeId))
                                {
                                    Data.Entities.Item IE = _dbContext.Item.Include(x => x.SubCategory)
                            .Include(x => x.SubCategory.Category)
                            .Include(x => x.Condition)
                            .Include(x => x.ItemStatus)
                            .Include(x => x.ItemFeedbacks).
                            Include(x => x.Likes)
                            .Where(x => x.Id == int.Parse(ItemsToExchangeId)).FirstOrDefault();
                                    if (IE != null)
                                        itemExchange.ItemsToExchange.Add(new ItemModel()
                                        {
                                            Item = IE,
                                            User = users.Where(x => x.Id == IE.UserId).FirstOrDefault()
                                        });
                                }
                        }
                        item.ItemExchanges = itemExchanges;
                    }

                    itemModels.Add(new ItemModel()
                    {
                        Item = item,
                        User = users.Where(x => x.Id == item.UserId).FirstOrDefault()
                    });
                }

                return itemModels.ToArray();
            }

            return null;
        }

        public ItemModel[] MyExchanges(int userId)
        {
            Response res = ApiCall
                 .ApiGetObject("https://localhost:44363/", "api/user/getAllUsers/");

            if (res.Status == HttpStatusCode.OK)
            {
                Data.Entities.Item[] items = _dbContext.Item.Include(x => x.SubCategory)
                    .Include(x => x.SubCategory.Category)
                    .Where(x => x.UserId == userId)
                    .ToArray();

                UserInfo[] users = JsonConvert.DeserializeObject<UserInfo[]>(res.Body.ToString());

                List<ItemModel> itemModels = new List<ItemModel>();
                foreach (var item in items)
                {
                    List<ItemExchanges> itemExchanges = _dbContext.ItemExchanges.Where(x => x.ItemId == item.Id).ToList();
                    if (itemExchanges.Count > 0)
                    {
                        foreach (var itemExchange in itemExchanges)
                        {
                            string[] ItemsToExchangeIds = itemExchange.ItemsToExchangeIds.Split(";");
                            foreach (var ItemsToExchangeId in ItemsToExchangeIds)
                                if (!string.IsNullOrEmpty(ItemsToExchangeId))
                                {
                                    Data.Entities.Item IE = _dbContext.Item.Include(x => x.SubCategory)
                            .Include(x => x.SubCategory.Category)
                            .Where(x => x.Id == int.Parse(ItemsToExchangeId)).FirstOrDefault();
                                    if (IE != null)
                                        itemExchange.ItemsToExchange.Add(new ItemModel()
                                        {
                                            Item = IE,
                                            User = users.Where(x => x.Id == IE.UserId).FirstOrDefault()
                                        });
                                }
                        }
                        item.ItemExchanges = itemExchanges;

                        itemModels.Add(new ItemModel()
                        {
                            Item = item,
                            User = users.Where(x => x.Id == item.UserId).FirstOrDefault()
                        });

                    }
                }

                return itemModels.ToArray();
            }

            return null;
        }

        public Response DeleteItemByItemUserId(int itemId, int userId, string uploadPath)
        {
            try
            {
                Data.Entities.Item item = _dbContext.Item
                    .Include(x => x.ItemFeedbacks).
                    Include(x => x.Likes)
                    .Where(x => x.Id == itemId && x.UserId == userId).FirstOrDefault();

                if (item == null)
                    return new Response()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Article non trouvé."
                    };

                foreach (var img in item.Images.Split(";"))
                    if (!string.IsNullOrEmpty(img))
                        File.Delete(uploadPath + img);

                _dbContext.Item.Remove(item);

                foreach (var feedback in item.ItemFeedbacks)
                    _dbContext.ItemFeedback.Remove(feedback);

                foreach (var like in item.Likes)
                    _dbContext.ItemLike.Remove(like);

                _dbContext.SaveChanges();

                return new Response()
                {
                    Status = HttpStatusCode.OK
                };

            }
            catch (Exception e)
            {
                return new Response()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }

        }

        public Response DeleteItemByItemId(int itemId, string uploadPath)
        {
            try
            {
                Data.Entities.Item item = _dbContext.Item
                    .Include(x => x.ItemFeedbacks).
                    Include(x => x.Likes)
                    .Where(x => x.Id == itemId).FirstOrDefault();

                if (item == null)
                    return new Response()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Article non trouvé."
                    };

                foreach (var img in item.Images.Split(";"))
                    if (!string.IsNullOrEmpty(img))
                        File.Delete(uploadPath + img);

                _dbContext.Item.Remove(item);

                foreach (var feedback in item.ItemFeedbacks)
                    _dbContext.ItemFeedback.Remove(feedback);

                foreach (var like in item.Likes)
                    _dbContext.ItemLike.Remove(like);

                _dbContext.SaveChanges();

                return new Response()
                {
                    Status = HttpStatusCode.OK
                };

            }
            catch (Exception e)
            {
                return new Response()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }
        }

        public Response EditItem(Data.Entities.Item item)
        {
            try
            {
                Data.Entities.Item i = _dbContext.Item.Where(x => x.Id == item.Id).FirstOrDefault();
                if (i == null)
                    return new Response
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Une erreur s'est produite, article non trouvé."
                    };


                i.Title = item.Title;
                i.Description = item.Description;
                i.Images = item.Images;
                i.Price = item.Price;
                i.Exchange = item.Exchange;
                i.LastUpdatedTime = DateTime.Now;
                i.ExchangeWithCategoryId = item.ExchangeWithCategoryId;
                i.ExchangeWithSubCategoryId = item.ExchangeWithSubCategoryId;
                i.ConditionId = item.ConditionId;
                i.SubCategoryId = item.SubCategoryId;

                _dbContext.Item.Update(i);
                _dbContext.SaveChanges();

                return new Response
                {
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }
        }

        public Response DeleteItemImg(int itemId, string img, string uploadPath)
        {
            try
            {
                Data.Entities.Item i = _dbContext.Item.Where(x => x.Id == itemId).FirstOrDefault();
                if (i == null)
                    return new Response
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Une erreur s'est produite, article non trouvé."
                    };
                if (string.IsNullOrEmpty(img))
                    return new Response
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Une erreur s'est produite, image non trouvé."
                    };

                i.Images = i.Images.Replace(img + ";", "");

                _dbContext.Item.Update(i);
                _dbContext.SaveChanges();

                File.Delete(uploadPath + img);

                return new Response
                {
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }
        }

        public Response AddRemoveLike(ItemLike like)
        {
            try
            {
                ItemLike il = _dbContext.ItemLike.Where(x => x.ItemId == like.ItemId && x.UserId == like.UserId).FirstOrDefault();
                if (il == null)
                    _dbContext.ItemLike.Add(like);
                else
                    _dbContext.ItemLike.Remove(il);

                _dbContext.SaveChanges();
                return new Response
                {
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }

        }

        public Response AddItemExchange(int itemId, List<ItemModel> itemsForExchange)
        {
            try
            {
                if (itemsForExchange == null || itemsForExchange.Count == 0)
                    return new Response
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Une erreur s'est produite, aucun article selectionné pour l'échange."
                    };

                /** insert exchange item status **/
                if (_dbContext.ItemExchangeStatus.Find(1) == null)
                    _dbContext.ItemExchangeStatus.Add(new ItemExchangeStatus()
                    {
                        Status = "Accepté"
                    });
                if (_dbContext.ItemExchangeStatus.Find(2) == null)
                    _dbContext.ItemExchangeStatus.Add(new ItemExchangeStatus()
                    {
                        Status = "Rejeté"
                    });
                if (_dbContext.ItemExchangeStatus.Find(3) == null)
                    _dbContext.ItemExchangeStatus.Add(new ItemExchangeStatus()
                    {
                        Status = "En cours"
                    });

                Data.Entities.Item i = _dbContext.Item.Where(x => x.Id == itemId).FirstOrDefault();
                if (i == null)
                    return new Response
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Une erreur s'est produite, article non trouvé."
                    };

                string itemForExchangeIds = "";
                foreach (var item in itemsForExchange)
                    itemForExchangeIds += item.Item.Id + ";";

                ItemExchanges itemExchanges = new ItemExchanges
                {
                    ExchangeRequestTime = DateTime.Now,
                    ExchangeStatusId = 3,
                    ItemId = i.Id,
                    ItemsToExchangeIds = itemForExchangeIds
                };

                _dbContext.ItemExchanges.Add(itemExchanges);
                _dbContext.SaveChanges();

                return new Response
                {
                    Status = HttpStatusCode.OK
                };

            }
            catch (Exception e)
            {
                return new Response
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }
        }

        public Response RemoveItemExchange(int exchangeId)
        {
            return new Response
            {
                Status = HttpStatusCode.OK
            };
        }

        public Response ItemExchangeStatusUpdate(int exchangeId, int statusId)
        {
            return new Response
            {
                Status = HttpStatusCode.OK
            };
        }

        #region private methods

        private string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDE FGHIJ KLMNO PQRSTUV WXYZ 01 2345 6789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private DateTime RandomDay()
        {
            Random gen = new Random();
            DateTime start = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        #endregion


    }
}
