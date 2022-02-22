using System.Collections.Generic;

// review: this database model is very naive .. there is no time-based periods ?
namespace DailyPmsShared
{
    // review: mixing French/English terms in model
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
