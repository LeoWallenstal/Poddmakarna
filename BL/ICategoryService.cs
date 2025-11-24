using Models;

namespace BL
{
    public interface ICategoryService : IService<Category>
    {
        Task<bool> CategoryExistsAsync(string categoryName);
    }
}
