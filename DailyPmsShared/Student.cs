using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DailyPmsShared
{
    public class Student : MongoEntity
    {
        //public string StudentID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Street { get; set; }

        public int PostalCode { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Parent1 { get; set; }

        public string Parent2 { get; set; }

        public DateTime RegistrationDate { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string SchoolID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ClasseID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string PmsFileID { get; set; }
    }
}
