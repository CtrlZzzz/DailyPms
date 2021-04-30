using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsClient.Services;
using DailyPmsShared;
using Microsoft.AspNetCore.Components;

namespace DailyPmsClient.Pages
{
    public partial class StudentOverview
    {
        [Inject]
        public IStudentDataService StudentDataService { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public bool FixedHeader { get; set; } = true;

        public string SearchString { get; set; }  = "";

        protected override async Task OnInitializedAsync()
        {
            Students = await StudentDataService.GetAllStudentsAsync();
        }

        public bool FilterStudents(Student student)
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                return true;
            }
            if (student.FirstName.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (student.LastName.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (student.BirthDate.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
