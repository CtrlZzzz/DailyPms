using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace DailyPmsShared
{
    public class MongoEntity : IEntity
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string _id { get; set; }
    }
}
