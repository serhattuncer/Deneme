using Entities.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Abstract;

namespace Presentation.Controllers
{
    [Route("Claim")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ClaimController(IServiceManager manager)
        {
            _manager = manager;
        }

        [Authorize(Policy = "GetClaims")]
        [HttpGet("get-claims")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                Log.Information("İnformation Get Claims");
                var list = _manager.claim.GetAllClaims().Result.Where(w => w.IsDeleted == false).ToList();
                if (list is null)
                {
                    return NotFound("Veri Bulunamadı.");
                }



                return Ok(list);

            }
            catch (Exception ex)
            {
                Log.Error("Error Get Claims");
                throw new Exception("Error Message:{0}", ex);
            }

        }

        [Authorize(Policy = "GetClaimById")]
        [HttpGet("get-claim-by-id{id}")]
        public async Task<IActionResult> GetClaimById(int id)
        {
            try
            {
                Log.Information("İnformation Get ClaimById");
                var result = await _manager.claim.GetClaimById(id);
                if (result is null)
                {
                    return NotFound(id + "Numaralı Id'ye ait veri bulunamadı.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error Get ClaimById");
                throw new Exception("Error Message:{0}", ex); ;
            }

        }

        [Authorize(Policy = "CreateClaim")]
        [HttpPost("create-claim")]
        public async Task<IActionResult> CreateClaim(ClaimsDto claimsDto)
        {
            try
            {
                Log.Information("İnformation Create Claim");
                await _manager.claim.CreateClaim(claimsDto);
                return Ok();
            }
            catch (Exception ex)
            {

                Log.Error("Error Create Claim");
                throw new Exception("Error Message:{0}", ex);
            }

        }

        [Authorize(Policy = "UpdateClaim")]
        [HttpPut("update-claim")]
        public async Task<IActionResult> UpdateClaim(ClaimsDto claimsDto)
        {
            try
            {
                Log.Information("İnformation Update Claim");
                await _manager.claim.UpdateClaim(claimsDto);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Update Claim");
                throw new Exception("Error Message:{0}", ex);
            }

        }

        [Authorize(Policy = "DeleteClaim")]
        [HttpPost("delete-claim/{id}")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            try
            {
                Log.Information("İnformation Delete Claim");
                await _manager.claim.DeleteClaim(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Delete Claim");
                throw new Exception("Error Message:{0}", ex);
            }

        }
    }
}
