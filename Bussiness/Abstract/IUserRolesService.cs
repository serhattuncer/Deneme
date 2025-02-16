using Entities.ModelDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IUserRolesService
    {
        Task<List<UserRoles>> GetAllUserRoles();
        Task<UserRoles> GetUserRoleById(int id);
        Task DeleteUserRoleList(int UserId);
        Task CreateUserRoleList(UserRoleListDto userRolesListDto);
    }
}
