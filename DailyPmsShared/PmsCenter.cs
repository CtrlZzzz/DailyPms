using System.Collections.Generic;

namespace DailyPmsShared
{
    public class PmsCenter
    {
        public string CenterID { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public int PostalCode { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string DirectorID { get; set; }

        public List<string> PmsIDs { get; set; }

        public List<string> SchoolIDs { get; set; }
    }
}
