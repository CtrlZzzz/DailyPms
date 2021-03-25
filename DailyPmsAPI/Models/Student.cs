using System;

namespace DailyPmsAPI.Models
{
    public class Student
    {
        public string StudentId { get; set; }

        public string FirtsName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Street { get; set; }

        public int PostalCode { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Mother { get; set; }

        public string Father { get; set; }

        public string SchoolId { get; set; }

        public string ClasseId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string PmsFileId { get; set; }
    }
}
