using Models;

namespace DAL
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool>CategoryExistsAsync(string categoryName);
    }
}
