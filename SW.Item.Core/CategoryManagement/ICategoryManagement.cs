using System;
using System.Collections.Generic;
using System.Text;
using SW.Item.Data.Common;
using SW.Item.Data.Entities;

namespace SW.Item.Core.CategoryManagement
{
    public interface ICategoryManagement
    {
        Response BatchAddCategory();
        Response BatchAddSubCategory();

        Category GetCategory(int id);
        Category[] GetCategories();
    }
}
