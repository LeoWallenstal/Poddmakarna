using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{

    public abstract class BaseEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        protected BaseEntity() { }
    }
}
