using Microsoft.EntityFrameworkCore;
using SchoolManagment.Models;
using System;
namespace SchoolManagment.Data{


    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses {get;set;}
        public DbSet<Classroom> Classrooms{ get; set; }

    }
}