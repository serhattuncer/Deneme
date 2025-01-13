using AutoMapper;
using Entities.ModelDto;
using Entities.Models;
using Repositories.Contracts;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class ClaimService : IClaimService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        public ClaimService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }


        public async Task CreateClaim(ClaimsDto claimsDto)
        {
            try
            {
                var data = _mapper.Map<Claims>(claimsDto);
                await _manager.Claim.Create(data);
                await _manager.SaveChangesAsync();
            }
            catch (Exception e)
            {

                new Exception("message:" + e);
            }
        }

        public async Task DeleteClaim(int id)
        {
            var data = _manager.Claim.GetById(id).Result;
            data.IsDeleted = true;
            _manager.Claim.Delete(data);
            await _manager.SaveChangesAsync();
        }

        public async Task<List<Claims>> GetAllClaims()
        {
            return await _manager.Claim.GetAll();
        }

        public async Task<Claims> GetClaimById(int id)
        {
            return await _manager.Claim.GetById(id);
        }

        public async Task UpdateClaim(ClaimsDto claimsDto)
        {
            var data = _mapper.Map<Claims>(claimsDto);
            _manager.Claim.Update(data);
            await _manager.SaveChangesAsync();
        }
    }
}
