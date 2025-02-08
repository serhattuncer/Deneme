using AutoMapper;
using Entities.ModelDto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.SecurityTokenService;
using Microsoft.IdentityModel.Tokens;
using Repositories.Contracts;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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

        private SigningCredentials GetSigningCredentials()
        {
            var JwtSettings= _configuration.GetSection("JwtSettings");
            var enccodedkey= JwtSettings["secretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(enccodedkey));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtsettings = _configuration.GetSection("JwtSettings");
            var secret = _configuration["secretKey"];
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtsettings["ValidateIssue"],
                ValidAudience = jwtsettings["ValidateAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principal;

        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var decryptValidateIssuer = jwtSettings["ValidateIssue"];
            var decryptValidateAudience = jwtSettings["ValidateAudience"];
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["ValidateIssue"],
                audience: jwtSettings["ValidateAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(jwtSettings["Expire"])),
                signingCredentials: signingCredentials
                );
            return tokenOptions;
        }
        private string GenerateRefreshToken()
        {
            var randomnumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomnumber);
                return Convert.ToBase64String(randomnumber);
            }
        }
        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signinCredentials = GetSigningCredentials();
            var claims = await GetClaimsByUser(_users);
            var tokenoptions = GenerateTokenOptions(signinCredentials,claims);
            var refreshtoken = GenerateRefreshToken();
            _users.RefreshToken = refreshtoken;
            if(populateExp)_users.RefreshTokenExpiryTime=DateTime.Now.AddDays(7);
            var data = _mapper.Map<UsersDto>(_users);
            await _manager.users.UpdateUser(data);

            var accesToken = new JwtSecurityTokenHandler().WriteToken(tokenoptions);
            return new TokenDto
            {
                AccessToken = accesToken,
                RefreshToken = refreshtoken
            };
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

                throw new Exception("Error Message:" + e.Message);
            }

        }

        public async Task<List<Claim>> GetClaimsByUser(Users user)
        {

            var claims = new List<Claim>
            {
             new Claim(ClaimTypes.Name,user.UserName),
             new Claim(ClaimTypes.Email,user.Email),
             new Claim("uid",user.User_Id.ToString())
            };
            var GetUser = await _manager.users.GetUserById(user.User_Id);
            if (GetUser is null)
            {
                throw new InvalidOperationException("Kullanıcı Bulunamadı.");
            }
            var roles = _manager.userrole.GetAllUserRoles().Result.Where(x => x.User_Id == user.User_Id).ToList();
            foreach (var item in roles)
            {
                var role = _manager.role.GetRoleById(item.Role_Id).Result;
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
                var roleclaims = _manager.roleclaim.GetAllRoleClaims().Result.Where(x => x.Role_Id == role.Role_Id).ToList();
                foreach (var roleclaim in roleclaims)
                {
                    var roleclaim1 = _manager.claim.GetClaimById(roleclaim.Claims_Id).Result;
                    claims.Add(new Claim("Type", roleclaim1.ClaimsName));
                }

            }
            return claims;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user=_manager.users.GetUserByName(principal.Identity.Name).Result;
            if (user is null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new BadRequestException();
            }
            _users = user;
            return await CreateToken(populateExp:false);
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

        public async Task<bool> ValidateUser(LoginUserDto loginUserDto)
        {
            _users = await _manager.users.GetUserByName(loginUserDto.UserName);
            return await _manager.users.ValidateUser(loginUserDto);

        }

    }
}
