using BOs.Entities;
using DAO;

namespace Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ICategoryDAO _categoryDAO;

        public CategoryRepo(ICategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }

        public List<Category> GetCategories()
        {
            // Retrieve all categories using the DAO
            return _categoryDAO.GetCategories();
        }

        public Category GetCategory(string categoryId)
        {
            // Retrieve a specific category by ID using the DAO
            return _categoryDAO.GetCategory(categoryId);
        }
    }
}
