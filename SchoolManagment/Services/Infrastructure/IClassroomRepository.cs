using System;
using System.Collections.Generic;
using SchoolManagment.Data;
using SchoolManagment.Repositories;

namespace SchoolManagment.Services.Infrastructure
{
    public interface IClassroomRepository: IGenericRepository<Classroom>
    {
        IEnumerable<Classroom> GetAllClassroom();
        IEnumerable<Classroom> GetClassroomById(int id);
    }

}
