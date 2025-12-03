using DAL;
using Models;

namespace BL
{
    public abstract class EntityService<T, TRepository> : IService<T> 
        where T : BaseEntity
        where TRepository : IRepository<T>
    {
        protected readonly TRepository repository;

        protected EntityService(TRepository repository)
        {
            this.repository = repository;
        }

        public async Task InsertAsync(T entity)
        {
            await repository.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var result = await repository.DeleteAsync(entity);
            return result;        
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<bool> ReplaceAsync(T entity)
        {
            return await repository.ReplaceAsync(entity);
        }
    }
}
