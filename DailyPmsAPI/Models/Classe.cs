using System.Collections.Generic;

namespace DailyPmsAPI.Models
{
    public class Classe
    {
        public string ClasseId { get; set; }

        public string Name { get; set; }

        public string SchoolId { get; set; }

        public string ProfessorName { get; set; }

        public List<string> PmsIDs { get; set; }

        public List<string> StudentIDs { get; set; }
    }
}
