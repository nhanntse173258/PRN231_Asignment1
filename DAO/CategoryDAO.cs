using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CategoryDAO : ICategoryDAO
    {
        private readonly SilverJewelry2023DbContext _context;

        public CategoryDAO()
        {
            _context = new SilverJewelry2023DbContext();
        }

        public Category GetCategory(string categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
