using AutoMapper;
using Entities.ModelDto;
using Entities.Models;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        public UserService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }


        public async Task CreateUser(UsersDto usersDto)
        {
            try
            {
                var data = _mapper.Map<Users>(usersDto);
                await _manager.User.Create(data);
                await _manager.SaveChangesAsync();
            }
            catch (Exception e)
            {

                new Exception("message:" + e);
            }
        }

        public async Task DeleteUser(int id)
        {
            var data = _manager.User.GetById(id).Result;
            data.IsDeleted = true;
            _manager.User.Delete(data);
            await _manager.SaveChangesAsync();
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _manager.User.GetAll();
        }

        public async Task<Users> GetUserById(int id)
        {
            return await _manager.User.GetById(id);
        }

        public async Task UpdateUser(UsersDto usersDto)
        {
            var data = _mapper.Map<Users>(usersDto);
            _manager.User.Update(data);
            await _manager.SaveChangesAsync();
        }
    }
}
