using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Models
{

    public abstract class BaseEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        protected BaseEntity() { }
    }
}
