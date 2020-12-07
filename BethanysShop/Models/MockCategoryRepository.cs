using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysShop.Models
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> AllCategories =>
            new List<Category>
            {
                new Category {CategoryId = 1, CategoryName = "Fruit pies", Description = "All -fruity"},
                new Category {CategoryId = 2, CategoryName = "Cheese cakes", Description = "Cheesy cakes"},
                new Category {CategoryId = 3, CategoryName = "Seasonal pies", Description = "Get in season"}
            };
    }
}
