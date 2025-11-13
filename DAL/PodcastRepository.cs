using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MongoDB.Driver;

namespace DAL
{
    public class PodcastRepository : EntityRepository<Podcast> //implementerar IPodRepository sen
    {
        public PodcastRepository(IMongoCollection<Podcast> aCollection, MongoClient client) : base(aCollection, client){}

    }
}
