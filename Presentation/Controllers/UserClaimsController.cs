using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Abstract;

namespace Presentation.Controllers
{
    [Route("UserClaims")]
    [ApiController]
    public class UserClaimsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public UserClaimsController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet("get-userclaims")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                Log.Information("İnformation Get UserClaims");
                var list = _manager.userclaim.GetAllUserClaims().Result.Where(w => w.IsDeleted == false).ToList();
                if (list is null)
                {
                    return NotFound("Veri Bulunamadı.");
                }



                return Ok(list);

            }
            catch (Exception ex)
            {
                Log.Error("Error Get UserClaims");
                throw new Exception("Error Message:{0}", ex);
            }

        }


        [HttpGet("get-userclaim-by-id{id}")]
        public async Task<IActionResult> GetUserClaimById(int id)
        {
            try
            {
                Log.Information("İnformation Get UserClaimById");
                var result = await _manager.userclaim.GetUserClaimById(id);
                if (result is null)
                {
                    return NotFound(id + "Numaralı Id'ye ait veri bulunamadı.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error Get UserClaimById");
                throw new Exception("Error Message:{0}", ex); ;
            }

        }


        [HttpPost("create-userclaim")]
        public async Task<IActionResult> CreateUserClaim(UserClaimsDto userclaimsDto)
        {
            try
            {
                Log.Information("İnformation Create UserClaim");
                await _manager.userclaim.CreateUserClaim(userclaimsDto);
                return Ok();
            }
            catch (Exception ex)
            {

                Log.Error("Error Create UserClaim");
                throw new Exception("Error Message:{0}", ex);
            }

        }


        [HttpPut("update-userclaim")]
        public async Task<IActionResult> UpdateUserClaim(UserClaimsDto userclaimsDto)
        {
            try
            {
                Log.Information("İnformation Update UserClaim");
                await _manager.userclaim.CreateUserClaim(userclaimsDto);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Update UserClaim");
                throw new Exception("Error Message:{0}", ex);
            }

        }


        [HttpPost("delete-userclaim/{id}")]
        public async Task<IActionResult> DeleteUserClaim(int id)
        {
            try
            {
                Log.Information("İnformation Delete UserClaim");
                await _manager.userclaim.DeleteUserClaim(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Delete UserClaim");
                throw new Exception("Error Message:{0}", ex);
            }

        }
    }
}
