using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DailyPmsShared
{
    public class Class : MongoEntity
    {
        //public string ClasseID { get; set; }

        public string Name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string SchoolID { get; set; }

        public string ProfessorName { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> PmsIDs { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> StudentIDs { get; set; }
    }
}
