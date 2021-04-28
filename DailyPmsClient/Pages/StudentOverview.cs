using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;

namespace DailyPmsClient.Pages
{
    public partial class StudentOverview
    {
        protected override Task OnInitializedAsync()
        {

            //InitializeCountries();
            //InitializeJobCategories();
            InitializeStudents();

            return base.OnInitializedAsync();
        }

        public IEnumerable<Student> Students { get; set; }

        //private List<Country> Countries { get; set; }

        //private List<JobCategory> JobCategories { get; set; }

        //private void InitializeJobCategories()
        //{
        //    JobCategories = new List<JobCategory>()
        //    {
        //        new JobCategory{JobCategoryId = 1, JobCategoryName = "Pie research"},
        //        new JobCategory{JobCategoryId = 2, JobCategoryName = "Sales"},
        //        new JobCategory{JobCategoryId = 3, JobCategoryName = "Management"},
        //        new JobCategory{JobCategoryId = 4, JobCategoryName = "Store staff"},
        //        new JobCategory{JobCategoryId = 5, JobCategoryName = "Finance"},
        //        new JobCategory{JobCategoryId = 6, JobCategoryName = "QA"},
        //        new JobCategory{JobCategoryId = 7, JobCategoryName = "IT"},
        //        new JobCategory{JobCategoryId = 8, JobCategoryName = "Cleaning"},
        //        new JobCategory{JobCategoryId = 9, JobCategoryName = "Bakery"},
        //        new JobCategory{JobCategoryId = 9, JobCategoryName = "Bakery"}
        //    };
        //}

        //private void InitializeCountries()
        //{
        //    Countries = new List<Country>
        //    {
        //        new Country {CountryId = 1, Name = "Belgium"},
        //        new Country {CountryId = 2, Name = "Netherlands"},
        //        new Country {CountryId = 3, Name = "USA"},
        //        new Country {CountryId = 4, Name = "Japan"},
        //        new Country {CountryId = 5, Name = "China"},
        //        new Country {CountryId = 6, Name = "UK"},
        //        new Country {CountryId = 7, Name = "France"},
        //        new Country {CountryId = 8, Name = "Brazil"}
        //    };
        //}

        private void InitializeStudents()
        {
            var s1 = new Student
            {
                StudentID = "300000000000000000000001",
                FirstName = "Jean",
                LastName = "Duval",
                BirthDate = new DateTime(1995, 7, 3),
                Street = "Rue ValJean 7",
                PostalCode = 2400,
                City = "MegaCity",
                Phone = "012345678",
                Email = "JeanD@gmail.com",
                Parent1 = "Alice Lemercier",
                Parent2 = "Robert Duval",
                RegistrationDate = new DateTime(2019, 9, 2),
                SchoolID = "100000000000000000000001",
                ClasseID = "200000000000000000000001",
                PmsFileID = "300000000000000000000001"
            };

            var s2 = new Student
            {
                StudentID = "300000000000000000000002",
                FirstName = "Clara",
                LastName = "Smith",
                BirthDate = new DateTime(1995, 6, 22),
                Street = "Avenue du Champs Martien 666",
                PostalCode = 6666,
                City = "NewEve",
                Phone = "98765432",
                Email = "ClaraSmith@Hotmail.com",
                Parent1 = "Guy Smith",
                Parent2 = "Alain Deloin",
                RegistrationDate = new DateTime(2018, 9, 5),
                SchoolID = "100000000000000000000002",
                ClasseID = "200000000000000000000002",
                PmsFileID = "300000000000000000000002"
            };

            Students = new List<Student> { s1, s2 };
        }
    }
}
