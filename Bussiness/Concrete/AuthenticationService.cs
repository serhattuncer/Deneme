using AutoMapper;
using Entities.ModelDto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
       
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IServiceManager _manager;
        private Users? _users;
        public AuthenticationService(IMapper mapper, IConfiguration configuration, UserManager<Users> userManager, RoleManager<Roles> roleManager, IServiceManager manager)
        {
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _manager = manager;
        }



        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            throw new NotImplementedException();
        }

        public async Task<Users> GetbyUserName(string userName)
        {
            try
            {
                var result = _manager.users.GetUserByName(userName).Result;
                if (result == null)
                {
                    throw new Exception("User not found");
                }
                return result;
            }
            catch (Exception e)
            {

                throw new Exception("Error Message:"+e.Message);
            }
           
        }

        public async Task<List<Claim>> GetClaims(Users user)
        {
            var claims = new List<Claim>();
            var userRoles = _manager.userrole.GetAllUserRoles().Result.Where(w=>w.User_Id==user.User_Id).ToList();
            foreach (var item in userRoles)
            {
             var rolinfo = _manager.role.GetRoleById(item.Role_Id).Result;
             claims.Add(new Claim(ClaimTypes.Role, rolinfo.Name));
            }
            return claims;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUser(UsersDto usersDto)
        {
            try
            {
                var result = _manager.users.CreateUser(usersDto);
                if (result == null)
                {
                    throw new Exception("User not created");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error Message:" + e.Message);
            }
           
        }

        public async Task<bool> ValidateUser(LoginUserDto loginUserDto )
        {
           _users = await _manager.users.GetUserByName(loginUserDto.UserName);
           return await _manager.users.ValidateUser(loginUserDto);
            
        }
    }
}
