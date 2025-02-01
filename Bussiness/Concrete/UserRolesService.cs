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
    public class UserRolesService : IUserRolesService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        public UserRolesService(IRepositoryManager manager, IMapper mapper)
        {
            _mapper = mapper;
            _manager = manager;
        }
        public async Task CreateUserRole(UserRolesDto userrolesDto)
        {
            try
            {
                var data = _mapper.Map<UserRoles>(userrolesDto);
                await _manager.UserRoles.Create(data);
                await _manager.SaveChangesAsync();
            }
            catch (Exception e)
            {

                new Exception("message:" + e);
            }
        }

        public async Task DeleteUserRole(int id)
        {
            var data = _manager.UserRoles.GetById(id).Result;
            data.IsDeleted = true;
            _manager.UserRoles.Delete(data);
            await _manager.SaveChangesAsync();
        }

        public async Task<List<UserRoles>> GetAllUserRoles()
        {
            return await _manager.UserRoles.GetAll();
        }

        public async Task<UserRoles> GetUserRoleById(int id)
        {
            return await _manager.UserRoles.GetById(id);
        }

        public async Task UpdateUserRole(UserRolesDto userrolesDto)
        {
            var data = _mapper.Map<UserRoles>(userrolesDto);
            _manager.UserRoles.Update(data);
            await _manager.SaveChangesAsync();
        }
    }
}
