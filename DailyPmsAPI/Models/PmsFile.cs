using System.Collections.Generic;

namespace DailyPmsAPI.Models
{
    public class PmsFile
    {
        public string PmsFileID { get; set; }

        public string StudentID { get; set; }

        public List<string> ActivityIDs { get; set; }
    }
}
