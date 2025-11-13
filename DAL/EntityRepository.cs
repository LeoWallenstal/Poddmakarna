using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MongoDB.Driver;

namespace DAL
{
    public abstract class EntityRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;
        private readonly MongoClient _client;
        protected EntityRepository(IMongoCollection<T> aCollection, MongoClient client)
        {
            _collection = aCollection;
            _client = client;
        }

        public async Task InsertAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            var result = await _collection.DeleteOneAsync(filter);

            return (result.DeletedCount > 0);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(FilterDefinition<T>.Empty).ToListAsync();
        }

        public async Task<bool> ReplaceAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            var result = await _collection.ReplaceOneAsync(filter, entity);

            return (result.MatchedCount > 0 || result.ModifiedCount > 0);
        }
    }
}
