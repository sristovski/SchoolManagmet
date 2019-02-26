using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagment.Models
{
    public class Classroom
    {
        public int Id { get; set; }

        [Required]
        public string ClassroomName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
        public Course Course { get; set; }
        public int SelectedCourse { get; set; }
    }

    public class ClassroomDataView
    {
        public List<Classroom> list { get; set; }
        public List<Course> courseList { get; set; }
    }
}
