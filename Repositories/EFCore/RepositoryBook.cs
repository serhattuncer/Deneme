using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryBook:GenericRepository<Book>,IRepositoryBook
    {
        private readonly RepositoryContext _context;
        public RepositoryBook(RepositoryContext context):base(context) 
        {
            _context = context;
        }

       
    }
}
