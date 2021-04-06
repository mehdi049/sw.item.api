using SW.Item.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SW.Item.Data;
using Microsoft.EntityFrameworkCore;
using SW.Item.Data.Entities;

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

            try
            {
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
                        AddedTime = DateTime.Now,
                        UserId = rnd.Next(1, 6),
                        Images = img
                    };
                    if (item.Exchange)
                    {
                        item.ExchangeWithCategory = rnd.Next(1, 7);
                        item.ExchangeWithSubCategory = rnd.Next(1, 3);
                    }
                    items.Add(item);
                }

                items.ForEach(l => _dbContext.Entry(l).State = EntityState.Added);
                _dbContext.Item.AddRange(items);
                _dbContext.SaveChanges();


                List<Data.Entities.ItemFeedback> itemFeedbacks = new List<Data.Entities.ItemFeedback>();
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


        private string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJ KLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Data.Entities.Item GetItem(int id)
        {
            return _dbContext.Item.Include(x => x.SubCategory)
                .Include(x => x.SubCategory.Category)
                .Include(x=>x.Condition)
                .Include(x=>x.ItemFeedbacks)
                .FirstOrDefault();
        }

        public Data.Entities.Item[] GetItems()
        {
            return _dbContext.Item.Include(x => x.SubCategory)
                .Include(x => x.SubCategory.Category)
                .Include(x => x.Condition)
                .Include(x => x.ItemFeedbacks)
                .ToArray();
        }
    }
}
