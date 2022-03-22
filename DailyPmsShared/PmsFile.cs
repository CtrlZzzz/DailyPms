using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DailyPmsShared
{
    public class PmsFile : MongoEntity
    {
        //public string PmsFileID { get; set; }

        public string StudentID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> ActivityIDs { get; set; }
    }
}
