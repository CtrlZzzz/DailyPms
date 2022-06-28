using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DailyPmsShared
{
    public class Agent : MongoEntity
    {
        //To remove
        public string AgentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Profession { get; set; }
        //

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public string JobTitle { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [JsonPropertyName("extension_c246e9c23ce1493ca1e31d49b433dbf9_PmsCenterName")]
        public string CenterName { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CenterID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> SchoolIDs { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> PlanningIDs { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> DailyIDs { get; set; }
    }
}
