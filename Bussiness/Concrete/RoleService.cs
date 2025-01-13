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
    public class RoleService : IRoleService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        public RoleService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task CreateRole(RolesDto rolesDto)
        {
            try
            {
                var data = _mapper.Map<Roles>(rolesDto);
                await _manager.Role.Create(data);
                await _manager.SaveChangesAsync();
            }
            catch (Exception e)
            {

                new Exception("message:" + e);
            }
        }

        public async Task DeleteRole(int id)
        {
            var data = _manager.Role.GetById(id).Result;
            data.IsDeleted = true;
            _manager.Role.Delete(data);
            await _manager.SaveChangesAsync();
        }

        public async Task<List<Roles>> GetAllRoles()
        {
            return await _manager.Role.GetAll();
        }

        public async Task<Roles> GetRoleById(int id)
        {
            return await _manager.Role.GetById(id);
        }

        public async Task UpdateRole(RolesDto rolesDto)
        {
            var data = _mapper.Map<Roles>(rolesDto);
            _manager.Role.Update(data);
            await _manager.SaveChangesAsync();
        }
    }
}
