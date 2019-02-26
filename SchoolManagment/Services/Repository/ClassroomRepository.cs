using System;
using SchoolManagment.Data;
using SchoolManagment.Repositories;
using SchoolManagment.Services.Infrastructure;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SchoolManagment.Services.Repository
{
    public class ClassroomRepository:GenericRepository<Classroom>, IClassroomRepository
    {
        private readonly SchoolContext _db;
        public ClassroomRepository(SchoolContext dbContext)
        {
            _dbContext = dbContext;
            _db = dbContext;
        }

        public IEnumerable<Classroom> GetAllClassroom()
        {
            var classrooms = _dbContext.Classrooms.Include(c => c.Courses).Select(p => p);

            return classrooms;
                
        }

        public IEnumerable<Classroom> GetClassroomById(int id)
        {
            var classrooms = _dbContext.Classrooms.Include(c => c.Courses).Where(x=> x.ClassroomId==id).Select(p => p);

            return classrooms;

        }
    }
}
