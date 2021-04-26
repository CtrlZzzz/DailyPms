using System.Collections.Generic;

namespace DailyPmsShared
{
    public class Classe
    {
        public string ClasseID { get; set; }

        public string Name { get; set; }

        public string SchoolID { get; set; }

        public string ProfessorName { get; set; }

        public List<string> PmsIDs { get; set; }

        public List<string> StudentIDs { get; set; }
    }
}
