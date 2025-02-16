
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
       
        [HttpPost("get-user-claims-by-userid/{id}")]
        public async Task<IActionResult> GetListByUserId(int id)
        {
            try
            {
                Log.Information("İnformation Get UserClaims");
                var list =  _manager.userclaim.GetAllUserClaims().Result.Where(w => w.IsDeleted == false&& w.User_Id==id).ToList();
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


        [HttpPost("create-userclaim-list")]
        public async Task<IActionResult> CreateUserClaim(UserClaimsListDto userclaimsDto)
        {
            try
            {
                Log.Information("İnformation Create UserClaim");
                await _manager.userclaim.CreateUserClaimList(userclaimsDto);
                return Ok();
            }
            catch (Exception ex)
            {

                Log.Error("Error Create UserClaim");
                throw new Exception("Error Message:{0}", ex);
            }

        }


        [HttpPost("delete-userclaim/{id}")]
        public async Task<IActionResult> DeleteUserClaimList(int id)
        {
            try
            {
                Log.Information("İnformation Delete UserClaim");
                await _manager.userclaim.DeleteUserClaimList(id);
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
