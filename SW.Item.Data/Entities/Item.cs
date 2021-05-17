using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.Item.Data.Entities
{
    public class Item
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public float? Price { get; set; }
        public bool Exchange { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime? LastUpdatedTime { get; set; }

        public int Seen { get; set; }

        public int? ExchangeWithCategoryId { get; set; }
        [NotMapped]
        public Category ExchangeWithCategory { get; set; }

        public int? ExchangeWithSubCategoryId { get; set; }
        [NotMapped]
        public SubCategory ExchangeWithSubCategory { get; set; }

        public int UserId { get; set; }

        public int ConditionId { get; set; }
        public ItemCondition Condition { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

        public int ItemStatusId { get; set; }
        public ItemStatus ItemStatus { get; set; }

        public List<ItemFeedback> ItemFeedbacks { get; set; }

        public List<ItemLike> Likes { get; set; }

    }
}
