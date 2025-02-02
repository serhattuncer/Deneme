using Entities.ModelDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IUserService
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUserById(int id);
        Task<Users> GetUserByName(string userName);
        Task CreateUser(UsersDto usersDto);
        Task UpdateUser(UsersDto usersDto);
        Task DeleteUser(int id);
        Task <bool> ValidateUser(LoginUserDto loginUserDto);
    }
}
