using Entities.ModelDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IClaimService
    {
        Task<List<Claims>> GetAllClaims();
        Task<Claims> GetClaimById(int id);
        Task CreateClaim(ClaimsDto claimsDto);
        Task UpdateClaim(ClaimsDto claimsDto);
        Task DeleteClaim(int id);
    }
}
