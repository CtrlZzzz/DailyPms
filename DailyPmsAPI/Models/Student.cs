using System;

namespace DailyPmsAPI.Models
{
    public class Student
    {
        public string StudentID { get; set; }

        public string FirtsName { get; set; }

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

        public string SchoolID { get; set; }

        public string ClasseID { get; set; }

        public string PmsFileID { get; set; }
    }
}
