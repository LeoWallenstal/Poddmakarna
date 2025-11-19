using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL
{
    public class PodcastRepository : EntityRepository<Podcast>, IPodRepository //implementerar IPodRepository sen
    {
        public PodcastRepository(IMongoCollection<Podcast> aCollection, MongoClient client) 
            : base(aCollection, client){}

        public async Task<bool> RssExistsAsync(string rssUrl)
        {
            var filter = Builders<Podcast>.Filter.Eq(p => p.RssUrl, rssUrl);
            var count = await _collection.CountDocumentsAsync(filter);
            return count > 0;
        }

        public async Task<bool> UpdateTitleAsync(Podcast podcast, string newTitle)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                try
                {
                    var filter = Builders<Podcast>.Filter.Eq(p => p.RssUrl, podcast.RssUrl);
                    var update = Builders<Podcast>.Update.Set(p => p.Title, newTitle);
                    var result = await _collection.UpdateOneAsync(filter, update);

                    await session.CommitTransactionAsync();

                    return result.MatchedCount > 0 || result.ModifiedCount > 0;
                }
                catch (Exception ex) {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }

        public async Task<List<Podcast>> GetByCategoryAsync(ObjectId categoryId) {
            var filter = Builders<Podcast>.Filter.Eq(p => p.Category, categoryId);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateCategoryAsync(Podcast podcast, ObjectId categoryId)
        {
            using (var session = await _client.StartSessionAsync()) {
                session.StartTransaction();
                try
                {
                    var filter = Builders<Podcast>.Filter.Eq(p => p.Id, podcast.Id);
                    var update = Builders<Podcast>.Update.Set(p => p.Category, categoryId);
                    var result = await _collection.UpdateOneAsync(filter, update);

                    await session.CommitTransactionAsync();

                    return result.MatchedCount > 0 || result.ModifiedCount > 0;
                }
                catch (Exception ex) {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }
    }
}
