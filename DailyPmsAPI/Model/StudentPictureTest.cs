using System.ComponentModel.DataAnnotations;

namespace DailyPmsApi.Model
{
    public class StudentPictureTest
    {
        [Key]
        public string _id { get; set; }
        public string Url { get; set; }
    }
}

