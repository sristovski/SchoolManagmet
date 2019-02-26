using System;
using SchoolManagment.Data;
using SchoolManagment.Repositories;
using SchoolManagment.Services.Infrastructure;

namespace SchoolManagment.Services.Repository
{
    public class StudentRepository:GenericRepository<Student>, IStudentRepository
    {

        public StudentRepository(SchoolContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
