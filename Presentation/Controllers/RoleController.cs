using Entities.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Serilog;
using Services.Abstract;

namespace Presentation.Controllers
{
    [Route("Role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public RoleController(IServiceManager manager)
        {
            _manager = manager;
        }

        [Authorize(Policy = "GetRoles")]
        [HttpGet("get-roles")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                Log.Information("İnformation Get Roles");
                var list = _manager.role.GetAllRoles().Result.Where(w => w.IsDeleted == false).ToList();
                if (list is null)
                {
                    return NotFound("Veri Bulunamadı.");
                }



                return Ok(list);

            }
            catch (Exception ex)
            {
                Log.Error("Error Get Roles");
                throw new Exception("Error Message:{0}", ex);
            }

        }

       [Authorize(Policy = "GetRoleById")]
        [HttpGet("get-role-by-id{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                Log.Information("İnformation Get RoleById");
                var result = await _manager.role.GetRoleById(id);
                if (result is null)
                {
                    return NotFound(id + "Numaralı Id'ye ait veri bulunamadı.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error Get RoleById");
                throw new Exception("Error Message:{0}", ex); ;
            }

        }

        [Authorize(Policy = "CreateRole")]
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(RolesDto rolesDto)
        {
            try
            {
                Log.Information("İnformation Create Role");
                await _manager.role.CreateRole(rolesDto);
                return Ok();
            }
            catch (Exception ex)
            {

                Log.Error("Error Create Role");
                throw new Exception("Error Message:{0}", ex);
            }

        }

        [Authorize(Policy = "UpdateRole")]
        [HttpPut("update-role")]
        public async Task<IActionResult> UpdateRole(RolesDto rolesDto)
        {
            try
            {
                Log.Information("İnformation Update Role");
                await _manager.role.UpdateRole(rolesDto);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Update Role");
                throw new Exception("Error Message:{0}", ex);
            }

        }

        [Authorize(Policy = "DeleteRole")]
        [HttpPost("delete-role/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                Log.Information("İnformation Delete Role");
                await _manager.role.DeleteRole(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Delete Role");
                throw new Exception("Error Message:{0}", ex);
            }

        }
    }
}
