using System;
using SchoolManagment.Data;
using SchoolManagment.Repositories;
using SchoolManagment.Services.Infrastructure;

namespace SchoolManagment.Services.Repository
{
    public class CourseRepository:GenericRepository<Course>, ICourseRepository
    {

        public CourseRepository(SchoolContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
