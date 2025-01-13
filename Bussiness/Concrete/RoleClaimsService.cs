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
    public class RoleClaimsService : IRoleClaimsService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public RoleClaimsService(IRepositoryManager manager, IMapper mapper)
        {
            _mapper = mapper;
            _manager = manager;
        }
        public async Task CreateRoleClaim(RoleClaimsDto roleclaimsDto)
        {
            try
            {
                var data = _mapper.Map<RoleClaims>(roleclaimsDto);
                await _manager.RoleClaims.Create(data);
                await _manager.SaveChangesAsync();
            }
            catch (Exception e)
            {

                new Exception("message:" + e);
            }
        }

        public async Task DeleteRoleClaim(int id)
        {
            var data = _manager.RoleClaims.GetById(id).Result;
            data.IsDeleted = true;
            _manager.RoleClaims.Delete(data);
            await _manager.SaveChangesAsync();
        }

        public async Task<List<RoleClaims>> GetAllRoleClaims()
        {
            return await _manager.RoleClaims.GetAll();
        }

        public async Task<RoleClaims> GetRoleClaimById(int id)
        {
            return await _manager.RoleClaims.GetById(id);
        }

        public async Task UpdateRoleClaim(RoleClaimsDto roleclaimsDto)
        {
            var data = _mapper.Map<RoleClaims>(roleclaimsDto);
            _manager.RoleClaims.Update(data);
            await _manager.SaveChangesAsync();
        }
    }
}
