using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
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
                List<Category> categories = new List<Category>();
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
            catch
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
            try
            {
                List<SubCategory> subCategories = new List<SubCategory>();

                // Vehicule
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 1,
                    Name = "Voiture"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 1,
                    Name = "Moto"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 1,
                    Name = "Bicyclette"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 1,
                    Name = "Camion"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 1,
                    Name = "SUV"
                });

                // Loisir
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 2,
                    Name = "CD"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 2,
                    Name = "Guitar"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 2,
                    Name = "Piano"
                });

                // Electronique
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 3,
                    Name = "Ordinateur / PC"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 3,
                    Name = "Tablette"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 3,
                    Name = "Composant PC"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 3,
                    Name = "Télephone"
                });

                // Maison et jardin
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 4,
                    Name = "Electroménager et vaisselles"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 4,
                    Name = "Meubles et décoration"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 4,
                    Name = "Jardin et outils de bricolage"
                });

                // Habillement
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 5,
                    Name = "Homme"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 5,
                    Name = "Femme"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 5,
                    Name = "Fille"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 5,
                    Name = "Garçon"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 5,
                    Name = "Bébé"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 5,
                    Name = "Unisex"
                });

                // Habillement
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 6,
                    Name = "Playstation"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 6,
                    Name = "Xbox"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 6,
                    Name = "Wii"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 6,
                    Name = "Nintendo"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 6,
                    Name = "DS"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 6,
                    Name = "PC"
                });

                // Livres
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 7,
                    Name = "Famille et bien-être"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 7,
                    Name = "Etudes supérieures"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 7,
                    Name = "Santé, forme et dietétique"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 7,
                    Name = "Religions et spiritualités"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 7,
                    Name = "Histoire"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 7,
                    Name = "Loisirs créatifs, décoration et passion"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 7,
                    Name = "Science humaine"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 7,
                    Name = "Sciences, technique et médecine"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 7,
                    Name = "Entreprise et bourse"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 7,
                    Name = "Romans policiers et polars"
                });
                subCategories.Add(new SubCategory()
                {
                    CategoryId = 7,
                    Name = "Romans et littérature"
                });

                subCategories.ForEach(l => _dbContext.Entry(l).State = EntityState.Added);
                _dbContext.SubCategory.AddRange(subCategories);
                _dbContext.SaveChanges();
                return new Response()
                {
                    Status = HttpStatusCode.OK
                };
            }
            catch
            {
                return new Response()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }
        }

        public Category[] GetAllCategories()
        {
            return _dbContext.Category.Include(x => x.SubCategories).ToArray();
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}
