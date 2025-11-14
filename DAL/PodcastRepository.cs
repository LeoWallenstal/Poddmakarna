using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MongoDB.Driver;

namespace DAL
{
    public class PodcastRepository : EntityRepository<Podcast>, IPodRepository//implementerar IPodRepository sen
    {
        public PodcastRepository(IMongoCollection<Podcast> aCollection, MongoClient client) : base(aCollection, client){}

        public async Task<bool> RssExistsAsync(string rssUrl)
        {
            var filter = Builders<Podcast>.Filter.Eq(p => p.RssUrl, rssUrl);
            var count = await _collection.CountDocumentsAsync(filter);
            return count > 0;
        }
    }
}
