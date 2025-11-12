using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MongoDB.Driver;

namespace DAL
{
    public class PodRepository : EntityRepository<Podcast> //implementerar IPodRepository sen
    {
        public PodRepository(IMongoCollection<Podcast> aCollection) : base(aCollection){}

    }
}
