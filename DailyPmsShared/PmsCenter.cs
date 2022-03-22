using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DailyPmsShared
{
    public class PmsCenter : MongoEntity
    {
        //public string CenterID { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public int PostalCode { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string DirectorID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> PmsIDs { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> SchoolIDs { get; set; }
    }
}
