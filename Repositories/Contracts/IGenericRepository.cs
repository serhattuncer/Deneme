using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IGenericRepository<T>
    {
        Task Create(T entity);
        T Update(T entity);
        T Delete(T entity);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);

    }
}
