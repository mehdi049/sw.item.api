using System;
using System.Collections.Generic;
using System.Text;

namespace SW.Item.Data.Entities
{
    public class SubCategory
    {
        public SubCategory()
        {
            Category= new Category();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
