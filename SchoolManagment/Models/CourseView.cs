using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagment.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string ShortName { get; set; }
        [Required]
        public string FullName { get; set; }
    }

    public class CourseDataView
    {
        public List<Course> list { get; set; }
    }
}
