using System.Collections.Generic;

namespace DailyPmsShared
{
    public class Agent
    {
        public string AgentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Profession { get; set; }

        public string CenterID { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        // review: Why flat DTOs ?
        public List<string> SchoolIDs { get; set; }

        public List<string> PlanningIDs { get; set; }

        public List<string> DailyIDs { get; set; }
    }
}
