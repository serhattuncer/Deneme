using AutoMapper;
using Entities.ModelDto;
using Entities.Models;
using Repositories.Contracts;
using Services.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class UserClaimsService : IUserClaimsService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public UserClaimsService(IRepositoryManager manager, IMapper mapper)
        {
            _mapper = mapper;
            _manager = manager;
        }

       

        public async Task CreateUserClaimList(UserClaimsListDto userClaimsListDto)
        {
            var list = GetAllUserClaims().Result.Where(w => w.IsDeleted == false && w.User_Id == userClaimsListDto.User_Id).ToList();
            //Gelen yetkileri mevcut yetkiler ile karşılaştırma 
            List<int> claimlist = new List<int>(userClaimsListDto.Claims_Id);
            List<int> claimsIds = list.Select(s => s.Claims_Id).ToList();
            foreach (var item in claimsIds)
            {
                if (!claimlist.Contains(item))
                {
                    //fronteenden gelen yetkilerde mevcut yetkileri aratma.
                    //mevcut yetkilerde olmayan yetkileri silme
                    var claims = list.Where(w => w.Claims_Id == item && w.User_Id == userClaimsListDto.User_Id).FirstOrDefault();
                    claims.IsDeleted = true;
                    _manager.UserClaims.Delete(claims);
                    await _manager.SaveChangesAsync();
                }
            }
            foreach (var item in claimlist)
            {
                if (!claimsIds.Contains(item))
                {
                    //fronteenden gelen yetkilerde mevcut yetkileri aratma.
                    //mevcut yetkilerde olmayan yetkileri silme
                    var claims = new UserClaims()
                    {
                        User_Id = userClaimsListDto.User_Id,
                        Claims_Id = item
                    };
                    await _manager.UserClaims.Create(claims);
                    await _manager.SaveChangesAsync();
                }
            }

        }

        

        public async Task DeleteUserClaimList(int UserId)
        {
            var claimslist = _manager.UserClaims.GetAll().Result.Where(w => w.User_Id == UserId).ToList();
            foreach (var item in claimslist)
            {
                _manager.UserClaims.Delete(item);
                await _manager.SaveChangesAsync();
            }

        }

        public async Task<List<UserClaims>> GetAllUserClaims()
        {
            return await _manager.UserClaims.GetAll();
        }

        public async Task<UserClaims> GetUserClaimById(int id)
        {
            return await _manager.UserClaims.GetById(id);
        }

       
    }
}