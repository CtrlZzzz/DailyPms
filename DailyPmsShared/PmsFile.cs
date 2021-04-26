using System.Collections.Generic;

namespace DailyPmsShared
{
    public class PmsFile
    {
        public string PmsFileID { get; set; }

        public string StudentID { get; set; }

        public List<string> ActivityIDs { get; set; }
    }
}
