using Microsoft.EntityFrameworkCore;
using SW.Item.Data.Entities;

namespace SW.Item.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Entities.Item> Item { get; set; }
        public DbSet<ItemCondition> ItemCondition { get; set; }
        public DbSet<ItemStatus> ItemStatus { get; set; }
        public DbSet<ItemFeedback> ItemFeedback { get; set; }
        public DbSet<ItemLike> ItemLike { get; set; }
        public DbSet<ItemExchanges> ItemExchanges { get; set; }
        public DbSet<ItemExchangeStatus> ItemExchangeStatus { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

    }
}
