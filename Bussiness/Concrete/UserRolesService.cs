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

        public async Task CreateUserRoleList(UserRoleListDto userRolesListDto)
        {
            var list = GetAllUserRoles().Result.Where(w => w.IsDeleted == false && w.User_Id == userRolesListDto.User_Id).ToList();
            //Gelen rolleri mevcut roller ile karşılaştırma 
            List<int> Rolelist = new List<int>(userRolesListDto.Role_Id);
            List<int> rolesIds = list.Select(s => s.Role_Id).ToList();
            foreach (var item in rolesIds)
            {
                if (!Rolelist.Contains(item))
                {
                    //fronteenden gelen rollerde mevcut rolleri aratma.
                    //mevcut rollerde olmayan rolleri silme
                    var roles = list.Where(w => w.Role_Id == item && w.User_Id == userRolesListDto.User_Id).FirstOrDefault();
                    roles.IsDeleted = true;
                    _manager.UserRoles.Delete(roles);
                    await _manager.SaveChangesAsync();
                }
            }
            foreach (var item in Rolelist)
            {
                if (!rolesIds.Contains(item))
                {
                    //fronteenden gelen rollerde mevcut rolleri aratma.
                    //mevcut rollerde olmayan rolleri silme
                    var roles = new UserRoles()
                    {
                        User_Id = userRolesListDto.User_Id,
                        Role_Id = item
                    };
                    await _manager.UserRoles.Create(roles);
                    await _manager.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteUserRole(int id)
        {
            var data = _manager.UserRoles.GetById(id).Result;
            data.IsDeleted = true;
            _manager.UserRoles.Delete(data);
            await _manager.SaveChangesAsync();
        }

        public async Task DeleteUserRoleList(int UserId)
        {
            var rolelist = _manager.UserRoles.GetAll().Result.Where(w => w.User_Id == UserId).ToList();
            foreach (var item in rolelist)
            {
                _manager.UserRoles.Delete(item);
                await _manager.SaveChangesAsync();
            }
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
