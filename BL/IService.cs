using Models;


namespace BL
{
    public interface IService<T> where T : BaseEntity
    {
        Task InsertAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<bool> ReplaceAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
