using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MongoDB.Driver;

namespace DAL
{
    public class CategoryRepository : EntityRepository<Category>, ICategoryRepository //eventuellt ärva nån sorts ICategoryRepository
    {
        public CategoryRepository(IMongoCollection<Category> aCollection, MongoClient client) 
            : base(aCollection, client){}

        public async Task<bool> CategoryExistsAsync(string categoryName)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Text, categoryName);
            return await _collection.Find(filter).AnyAsync();
        }

        //Category-specifika metoder här, eventuellt
    }
}
