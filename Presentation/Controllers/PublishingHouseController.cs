using Entities.ModelDto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Serilog;
using Services.Abstract;

namespace Presentation.Controllers
{
    [Route("PublishingHouse")]
    [ApiController]
    public class PublishingHouseController : ControllerBase
    {
        private readonly IServiceManager _manager;


        public PublishingHouseController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("get-publishinghouses")]
        public async Task<IActionResult> GetAllList()
        {
            try
            {
                Log.Information("İnformation Get PublishingHouses");
                var list = _manager.publishingHouse.GetAllPublishingHouses().Result.Where(w => w.IsDeleted == false).ToList();
                if (list is null)
                    NotFound("Veri Bulunamadı");

                return Ok(list);
            }
            catch (Exception ex)
            {

                Log.Error("Error Get PublishingHouses");
                throw new Exception("Error Message:{0}", ex);
            }

        }


        [HttpGet("get-publishinghouse-by-id{id}")]
        public async Task<IActionResult> GetPublishingHouseById(int id)
        {
            try
            {
                Log.Information("İnformation Get PublishingHouseById");
                var result = await _manager.publishingHouse.GetPublishingHouseById(id);
                if (result is null)
                    return NotFound(id + "Numaralı Id'ye ait veri bulunamadı.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error Get PublishingHouseById");
                throw new Exception("Error Message:{0}", ex);
            }

        }


        [HttpPost("create-publishinghouse")]
        public async Task<IActionResult> CreatePublishingHouse(PublishingHouseDto PublishinghouseDto)
        {
            try
            {
                Log.Information("İnformation Create PublishingHouse");
                await _manager.publishingHouse.CreatePublishingHouse(PublishinghouseDto);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error("Error Create PublishingHouse");
                throw new Exception("Error Message:{0}", ex);
            }
            
        }


        [HttpPut("update-publishinghouse")]
        public async Task<IActionResult> UpdatePublishingHouse(PublishingHouseDto PublishinghouseDto)
        {
            try
            {
                Log.Information("İnformation Update PublishingHouse");
                await _manager.publishingHouse.UpdatePublishingHouse(PublishinghouseDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Error Update PublishingHouse");
                throw new Exception("Error Message:{0}", ex);
            }
            
        }


        [HttpPost("delete-publishinghouse/{id}")]
        public async Task<IActionResult> DeletePublishingHouse(int id)
        {
            try
            {
                Log.Information("İnformation Delete PublishingHouse");
                await _manager.publishingHouse.DeletePublishingHouse(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Error Delete PublishingHouse");
                throw new Exception("Error Message:{0}", ex);
            }
           
        }

    }
}
