using Entities.ModelDto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IAuthenticationService
    {
        Task RegisterUser(UsersDto usersDto);
        Task<bool> ValidateUser(LoginUserDto loginUserDto);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
        Task<Users> GetbyUserName(string userName);
        Task<List<Claim>> GetClaimsByUser(Users user);   
        
    }
}
