using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EFCore
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RepositoryContext _context;

        public GenericRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task Create(T entity)
        {
           _context.Set<T>().Add(entity);
        }

        public T Delete(T entity)
        {
             _context.Set<T>().Update(entity);
            return entity;
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
            
        }

        public async Task<T> GetById(int id)
        {
           var entity = _context.Set<T>().Find(id);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<T> GetByName(string userName)
        {
            var entity = _context.Set<T>().Find(userName);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
    }
}
