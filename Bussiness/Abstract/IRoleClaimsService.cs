using Entities.ModelDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IRoleClaimsService
    {
        Task<List<RoleClaims>> GetAllRoleClaims();
        Task<RoleClaims> GetRoleClaimById(int id);
        Task CreateRoleClaim(RoleClaimsDto roleclaimsDto);
        Task UpdateRoleClaim(RoleClaimsDto roleclaimsDto);
        Task DeleteRoleClaim(int id);
    }
}
