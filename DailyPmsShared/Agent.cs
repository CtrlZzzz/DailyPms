using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DailyPmsShared
{
    public class Agent : MongoEntity
    {
        //public string AgentId { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        //public string Profession { get; set; }


        public string GivenName { get; set; }

        public string Surname { get; set; }

        public string JobTitle { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [JsonPropertyName("extension_1223f9ee-5ef7-4019-9eb4-d0d68f8ff236_PmsCenterName")]
        public string CenterName { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CenterID { get; set; }

        [JsonPropertyName("User's Object ID")]
        public string UserFlowObjectID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> SchoolIDs { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> PlanningIDs { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> DailyIDs { get; set; }
    }
}
