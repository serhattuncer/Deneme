using Entities.ModelDto;
using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryUsers : GenericRepository<Users>, IRepositoryUsers
    {
        private readonly RepositoryContext _context;

        public RepositoryUsers(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public Task<bool> ValidateUser(LoginUserDto loginUserDto)
        {

            var data = _context.Users.FirstOrDefault(x => x.UserName == loginUserDto.UserName && x.PasswordHash == loginUserDto.Password);
            if(data is null) return Task.FromResult(false);
            return Task.FromResult(true);
        }
    }
}
