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
        Task DeleteRoleClaimList(int RoleId);
        Task CreateRoleClaimList(RoleClaimsListDto roleClaimsListDto);
    }
}
