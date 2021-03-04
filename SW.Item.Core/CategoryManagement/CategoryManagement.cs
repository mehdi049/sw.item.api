using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using SW.Item.Data;
using SW.Item.Data.Common;
using SW.Item.Data.Entities;

namespace SW.Item.Core.CategoryManagement
{
    public class CategoryManagement : ICategoryManagement
    {
        private ApplicationDbContext _dbContext;

        public CategoryManagement(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Response BatchAddCategory()
        {
            try
            {
                List<Category> categories= new List<Category>();
                categories.Add(new Category()
                {
                    Name = "Véhicules"
                });
                categories.Add(new Category()
                {
                    Name = "Loisirs"
                });
                categories.Add(new Category()
                {
                    Name = "Eléctronique"
                });
                categories.Add(new Category()
                {
                    Name = "Maison et jardin"
                });
                categories.Add(new Category()
                {
                    Name = "Habillement"
                });
                categories.Add(new Category()
                {
                    Name = "Jeux video"
                });
                categories.Add(new Category()
                {
                    Name = "Livres"
                });

                _dbContext.Category.AddRange(categories);
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

        public Response BatchAddSubCategory()
        {
            throw new NotImplementedException();
        }

        public Category[] GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}
