using System.ComponentModel.DataAnnotations;

namespace DailyPmsShared
{
    public class StudentPicture 
    {
        [Key]
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string BlobName { get; set; }
    }
}

