using System.ComponentModel.DataAnnotations;

namespace DailyPmsShared
{
    public class StudentPicture : IEntity
    {
        [Key]
        public string _id { get; set; }
        public string Url { get; set; }
    }
}

