using Models;

namespace DAL
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task InsertAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<bool> ReplaceAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
