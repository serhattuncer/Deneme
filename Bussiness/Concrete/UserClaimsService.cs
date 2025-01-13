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
    public class UserClaimsService : IUserClaimsService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public UserClaimsService( IRepositoryManager manager,IMapper mapper)
        {
            _mapper = mapper;
            _manager = manager;
        }

        public async Task CreateUserClaim(UserClaimsDto userclaimsDto)
        {
            try
            {
                var data = _mapper.Map<UserClaims>(userclaimsDto);
                await _manager.UserClaims.Create(data);
                await _manager.SaveChangesAsync();
            }
            catch (Exception e)
            {

                new Exception("message:" + e);
            }
        }

        public async Task DeleteUserClaim(int id)
        {
            var data = _manager.UserClaims.GetById(id).Result;
            data.IsDeleted = true;
            _manager.UserClaims.Delete(data);
            await _manager.SaveChangesAsync();
        }

        public async Task<List<UserClaims>> GetAllUserClaims()
        {
            return await _manager.UserClaims.GetAll();
        }

        public async Task<UserClaims> GetUserClaimById(int id)
        {
            return await _manager.UserClaims.GetById(id);
        }

        public async Task UpdateUserClaim(UserClaimsDto userclaimsDto)
        {
            var data = _mapper.Map<UserClaims>(userclaimsDto);
            _manager.UserClaims.Update(data);
            await _manager.SaveChangesAsync();
        }
    }
}
