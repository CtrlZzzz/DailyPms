using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DailyPmsAPI.Models
{
    public class School
    {
        [BsonId]
        public ObjectId SchoolId { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public int PostalCode { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string DirectorName { get; set; }

        public MongoDBRef PmsCenter { get; set; }

        public List<MongoDBRef> Classes { get; set; }

        public List<MongoDBRef> Students { get; set; }
    }
}
