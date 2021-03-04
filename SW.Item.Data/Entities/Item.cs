using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SW.Item.Data.Entities
{
    public class Item
    {
        public int Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [MaxLength(10)]
        [Required]
        public string Condition { get; set; }
        public string Images { get; set; }
        public float? Price { get; set; }
        public bool Exchange { get; set; }

        public string ExchangeWithCategory { get; set; }
        public string ExchangeWithSubCategory { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
