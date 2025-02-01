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
        [HttpGet("get-roleclaims")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                Log.Information("İnformation Get RoleClaims");
                var list = _manager.roleclaim.GetAllRoleClaims().Result.Where(w => w.IsDeleted == false).ToList();
                if (list is null)
                {
                    return NotFound("Veri Bulunamadı.");
                }



                return Ok(list);

            }
            catch (Exception ex)
            {
                Log.Error("Error Get RoleClaims");
                throw new Exception("Error Message:{0}", ex);
            }

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


        [HttpGet("get-roleclaim-by-id{id}")]
        public async Task<IActionResult> GetRoleClaimById(int id)
        {
            try
            {
                Log.Information("İnformation Get RoleClaimById");
                var result = await _manager.roleclaim.GetRoleClaimById(id);
                if (result is null)
                {
                    return NotFound(id + "Numaralı Id'ye ait veri bulunamadı.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error Get RoleClaimById");
                throw new Exception("Error Message:{0}", ex); ;
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


        [HttpPut("update-roleclaim")]
        public async Task<IActionResult> UpdateRoleClaim(RoleClaimsDto roleclaimsDto)
        {
            try
            {
                Log.Information("İnformation Update RoleClaim");
                await _manager.roleclaim.CreateRoleClaim(roleclaimsDto);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Update RoleClaim");
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
