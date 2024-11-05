using BOs.Entities;

namespace Repository
{
    public interface ICategoryRepo
    {
        public Category GetCategory(string categoryId);
        public List<Category> GetCategories();
    }
}
