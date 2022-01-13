using System;

namespace DailyPmsShared.DTOs
{
	public class SchoolSummary
	{
        public string SchoolID { get; set; }

        public string Name { get; set; }

        public string Moniker { get; set; }

        public string Street { get; set; }

        public int PostalCode { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string DirectorName { get; set; }
    }
}

