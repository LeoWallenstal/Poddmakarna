using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public abstract class EntityService<T> : IService<T> where T : BaseEntity
    {
        private readonly EntityRepository<T> repository;

        protected EntityService(EntityRepository<T> repository)
        {
            this.repository = repository;
        }

        public async Task InsertAsync(T entity)
        {
            //validering : )
            await repository.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            //validering : )
            var result = await repository.DeleteAsync(entity);
            return result;        
        }

        public async Task<List<T>> GetAllAsync()
        {
            //validering : )
            return await repository.GetAllAsync();
        }

        public async Task<bool> ReplaceAsync(T entity)
        {
            //validering : )
            return await repository.ReplaceAsync(entity);
        }
    }
}
