﻿using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<T> GetAsync(int id);

        IQueryable<T> Query();

        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);

        //Task DeleteAsync(int id);
    }
}