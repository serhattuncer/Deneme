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

        public async Task CreateRoleClaimList(RoleClaimsListDto roleClaimsListDto)
        {
            var list = GetAllRoleClaims().Result.Where(w => w.IsDeleted == false && w.Role_Id == roleClaimsListDto.Role_Id).ToList();
            //Gelen yetkileri mevcut yetkiler ile karşılaştırma 
            List<int> claimlist = new List<int>(roleClaimsListDto.Claims_Id);
            List<int> claimsIds = list.Select(s => s.Claims_Id).ToList();
            foreach (var item in claimsIds)
            {
                if (!claimlist.Contains(item))
                {
                    //fronteenden gelen yetkilerde mevcut yetkileri aratma.
                    //mevcut yetkilerde olmayan yetkileri silme
                    var claims = list.Where(w => w.Claims_Id == item && w.Role_Id == roleClaimsListDto.Role_Id).FirstOrDefault();
                    claims.IsDeleted = true;
                    _manager.RoleClaims.Delete(claims);
                    await _manager.SaveChangesAsync();
                }
            }
            foreach (var item in claimlist)
            {
                if (!claimsIds.Contains(item))
                {
                    //fronteenden gelen yetkilerde mevcut yetkileri aratma.
                    //mevcut yetkilerde olmayan yetkileri silme
                    var claims = new RoleClaims()
                    {
                        Role_Id = roleClaimsListDto.Role_Id,
                        Claims_Id = item
                    };
                    await _manager.RoleClaims.Create(claims);
                    await _manager.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteRoleClaim(int id)
        {
            var data = _manager.RoleClaims.GetById(id).Result;
            data.IsDeleted = true;
            _manager.RoleClaims.Delete(data);
            await _manager.SaveChangesAsync();
        }

        public async Task DeleteRoleClaimList(int RoleId)
        {
            var claimslist = _manager.RoleClaims.GetAll().Result.Where(w => w.Role_Id == RoleId).ToList();
            foreach (var item in claimslist)
            {
                _manager.RoleClaims.Delete(item);
                await _manager.SaveChangesAsync();
            }
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
