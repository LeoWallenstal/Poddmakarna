using DAL;
using Models;

namespace BL
{
    public class CategoryService : EntityService<Category, ICategoryRepository>, ICategoryService
    {
        public CategoryService(ICategoryRepository repository) 
            : base(repository){}

        public async Task<bool> CategoryExistsAsync(string categoryName)
        {
            return await repository.CategoryExistsAsync(categoryName);
        }
    }
}
