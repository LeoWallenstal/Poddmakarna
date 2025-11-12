using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
