using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DailyPmsShared
{
    public class Agent : MongoEntity
    {
        //public string AgentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Profession { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CenterID { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> SchoolIDs { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> PlanningIDs { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> DailyIDs { get; set; }


        //AD B2C claims :
        public string GivenName { get; set; }

        public string Surname { get; set; }

        public string JobTitle { get; set; }

        public string EmailAddress { get; set; }

        public string PMSCenter { get; set; }

        public string ObjectID { get; set; }
    }
}
