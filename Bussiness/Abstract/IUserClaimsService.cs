
using Entities.ModelDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IUserClaimsService
    {
        Task<List<UserClaims>> GetAllUserClaims();
        Task<UserClaims> GetUserClaimById(int id);
        Task DeleteUserClaimList(int UserId);
        Task CreateUserClaimList(UserClaimsListDto userClaimsListDto);
    }
}
