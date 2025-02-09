using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace Presentation.Controllers
{
    [Route("Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authentication;
        private readonly ILogger _logger;
        public AuthenticationController(IAuthenticationService authentication, ILogger logger)
        {
            _authentication = authentication;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginUserDto user)
        {
            if (!await _authentication.ValidateUser(user))
            {
                return Unauthorized();
            }
            var tokenDto = await _authentication.CreateToken(populateExp: true);
            return Ok(tokenDto);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UsersDto usersDto)
        {
            var user = _authentication.RegisterUser(usersDto);
            if (user.IsCompleted) return Ok();
            else return BadRequest();
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _authentication.RefreshToken(tokenDto); 
            return Ok(tokenDtoToReturn);
        }
        [HttpGet("get-claims")]
        public async Task<IActionResult> GetClaims(string username)
        {
            var user=await _authentication.GetbyUserName(username);
            var claims=await _authentication.GetClaimsByUser(user);
            return Ok(claims);
        }
    }
}
