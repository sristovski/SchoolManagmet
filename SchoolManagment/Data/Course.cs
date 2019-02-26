
using System.ComponentModel.DataAnnotations;

namespace SchoolManagment.Data
{
    public class Course:BaseEntity
    {
        public int CourseId { get; set; }
        [Required]
        public string ShortName { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}