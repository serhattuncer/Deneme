using Entities.ModelDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IRoleService
    {
        Task<List<Roles>> GetAllRoles();
        Task<Roles> GetRoleById(int id);
        Task CreateRole(RolesDto rolesDto);
        Task UpdateRole(RolesDto rolesDto);
        Task DeleteRole(int id);
    }
}
