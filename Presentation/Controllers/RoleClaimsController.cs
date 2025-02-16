using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Abstract;

namespace Presentation.Controllers
{
    [Route("RoleClaims")]
    [ApiController]
    public class RoleClaimsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public RoleClaimsController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpPost("get-role-claims-by-roleid/{id}")]
        public async Task<IActionResult> GetListByRoleId(int id)
        {
            try
            {
                Log.Information("İnformation Get UserClaims");
                var list = _manager.roleclaim.GetAllRoleClaims().Result.Where(w => w.IsDeleted == false && w.Role_Id == id).ToList();
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

        [HttpPost("create-roleclaim-list")]
        public async Task<IActionResult> CreateRoleClaim(RoleClaimsListDto roleclaimsDto)
        {
            try
            {
                Log.Information("İnformation Create RoleClaim");
                await _manager.roleclaim.CreateRoleClaimList(roleclaimsDto);
                return Ok();
            }
            catch (Exception ex)
            {

                Log.Error("Error Create RoleClaim");
                throw new Exception("Error Message:{0}", ex);
            }

        }

        [HttpPost("delete-roleclaim/{id}")]
        public async Task<IActionResult> DeleteRoleClaim(int id)
        {
            try
            {
                Log.Information("İnformation Delete RoleClaim");
                await _manager.roleclaim.DeleteRoleClaimList(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Delete RoleClaim");
                throw new Exception("Error Message:{0}", ex);
            }

        }
    }
}
