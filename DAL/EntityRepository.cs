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
        protected readonly IMongoCollection<T> _collection;
        protected readonly MongoClient _client;
        protected EntityRepository(IMongoCollection<T> aCollection, MongoClient client)
        {
            _collection = aCollection;
            _client = client;
        }

        public async Task InsertAsync(T entity)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                try
                {
                    await _collection.InsertOneAsync(entity);
                    await session.CommitTransactionAsync();
                }
                catch (Exception ex)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            using (var session = await _client.StartSessionAsync()) {
                session.StartTransaction();
                try
                {
                    var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
                    var result = await _collection.DeleteOneAsync(filter);
                    await session.CommitTransactionAsync();
                    return (result.DeletedCount > 0);
                }
                catch(Exception ex)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(FilterDefinition<T>.Empty).ToListAsync();
        }

        public async Task<bool> ReplaceAsync(T entity)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                try
                {
                    var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
                    var result = await _collection.ReplaceOneAsync(filter, entity);

                    await session.CommitTransactionAsync();

                    return (result.MatchedCount > 0 || result.ModifiedCount > 0);
                }
                catch (Exception ex) {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }
    }
}
