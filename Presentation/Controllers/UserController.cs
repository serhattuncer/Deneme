using Entities.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Migrations;
using Serilog;
using Services.Abstract;

namespace Presentation.Controllers
{
    [Route("User")]
    [ApiController]   
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public UserController(IServiceManager manager)
        {
            _manager = manager;
        }

        [Authorize(Policy = "GetUsers")]
        [HttpGet("get-users")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                Log.Information("İnformation Get Users");
                var list = _manager.users.GetAllUsers().Result.Where(w => w.IsDeleted == false).ToList();
                if (list is null)
                {
                    return NotFound("Veri Bulunamadı.");
                }
                return Ok(list);

            }
            catch (Exception ex)
            {
                Log.Error("Error Get Users");
                throw new Exception("Error Message:{0}", ex);
            }

        }

        [Authorize(Policy = "GetUserById")]
        [HttpGet("get-user-by-id{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                Log.Information("İnformation Get UserById");
                var result = await _manager.users.GetUserById(id);
                if (result is null)
                {
                    return NotFound(id + "Numaralı Id'ye ait veri bulunamadı.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error Get UserById");
                throw new Exception("Error Message:{0}", ex); ;
            }

        }

        [Authorize(Policy = "CreateUser")]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UsersDto usersDto)
        {
            try
            {
                Log.Information("İnformation Create User");
                await _manager.users.CreateUser(usersDto);
                return Ok();
            }
            catch (Exception ex)
            {

                Log.Error("Error Create User");
                throw new Exception("Error Message:{0}", ex);
            }

        }

        [Authorize(Policy = "UpdateUser")]
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UsersDto usersDto)
        {
            try
            {
                Log.Information("İnformation Update User");
                await _manager.users.UpdateUser(usersDto);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Update User");
                throw new Exception("Error Message:{0}", ex);
            }

        }

        [Authorize(Policy = "DeleteUser")]
        [HttpPost("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                Log.Information("İnformation Delete User");
                await _manager.users.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Delete User");
                throw new Exception("Error Message:{0}", ex);
            }

        }
    }
}
