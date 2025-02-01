using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Abstract;

namespace Presentation.Controllers
{
    [Route("UserRoles")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public UserRolesController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet("get-userroles")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                Log.Information("İnformation Get UserRoles");
                var list = _manager.userrole.GetAllUserRoles().Result.Where(w => w.IsDeleted == false).ToList();
                if (list is null)
                {
                    return NotFound("Veri Bulunamadı.");
                }



                return Ok(list);

            }
            catch (Exception ex)
            {
                Log.Error("Error Get UserRoles");
                throw new Exception("Error Message:{0}", ex);
            }

        }
        
        [HttpPost("get-user-roles-by-userroleid/{id}")]
        public async Task<IActionResult> GetListByUserRoleId(int id)
        {
            try
            {
                Log.Information("İnformation Get UserRolesById");
                var list = _manager.userrole.GetAllUserRoles().Result.Where(w => w.IsDeleted == false && w.User_Id == id).ToList();
                if (list is null)
                {
                    return NotFound("Veri Bulunamadı.");
                }

                return Ok(list);

            }
            catch (Exception ex)
            {
                Log.Error("Error Get İnformation Get UserRolesById");
                throw new Exception("Error Message:{0}", ex);
            }

        }


        [HttpGet("get-userrole-by-id/{id}")]
        public async Task<IActionResult> GetUserRoleById(int id)
        {
            try
            {
                Log.Information("İnformation Get UserRoleById");
                var result = await _manager.userrole.GetUserRoleById(id);
                if (result is null)
                {
                    return NotFound(id + "Numaralı Id'ye ait veri bulunamadı.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error Get UserRoleById");
                throw new Exception("Error Message:{0}", ex); ;
            }

        }


        [HttpPost("create-userrole-list")]
        public async Task<IActionResult> CreateUserRole(UserRoleListDto userRolesListDto)
        {
            try
            {
                Log.Information("İnformation Create UserRole");
                await _manager.userrole.CreateUserRoleList(userRolesListDto);
                return Ok();
            }
            catch (Exception ex)
            {

                Log.Error("Error Create UserRole");
                throw new Exception("Error Message:{0}", ex);
            }

        }


        [HttpPut("update-userrole")]
        public async Task<IActionResult> UpdateUserRole(UserRolesDto userrolesDto)
        {
            try
            {
                Log.Information("İnformation Update UserRole");
                await _manager.userrole.UpdateUserRole(userrolesDto);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Update UserRole");
                throw new Exception("Error Message:{0}", ex);
            }

        }


        [HttpPost("delete-userrole/{id}")]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            try
            {
                Log.Information("İnformation Delete UserRole");
                await _manager.userrole.DeleteUserRoleList(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Delete UserRole");
                throw new Exception("Error Message:{0}", ex);
            }

        }
    }
}
