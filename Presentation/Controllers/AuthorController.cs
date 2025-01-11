using Entities.ModelDto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Abstract;

namespace Presentation.Controllers
{
    [Route("Author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public AuthorController(IServiceManager manager)
        {
            _manager = manager;
        }


        [HttpGet("get-authors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                Log.Information("İnformation Get Authors");
                var list = _manager.author.GetAuthors().Result.Where(w => w.IsDeleted == false).ToList();
                if (list is null)
                    return NotFound("Veri Bulunamadı.");

                return Ok(list);
            }
            catch (Exception ex)
            {
                Log.Error("Error Get Authors");
                throw new Exception("Error Message:{0}", ex);
            }

        }


        [HttpGet("get-author-by-id{id}")]
        public async Task<IActionResult> GetOneAuthorById(int id)
        {
            try
            {
                Log.Information("İnformation Get AuthorById");
                var result = await _manager.author.GetAuthorById(id);
                if (result is null)
                    return NotFound(id + "Numaralı Id'ye ait veri bulunamadı.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error Get AuthorById");
                throw new Exception("Error Message:{0}", ex);
            }
           
        }


        [HttpPost("create-author")]
        public async Task<IActionResult> CreateAuthor(AuthorDto authorDto)
        {
            try
            {
                Log.Information("İnformation Create Author");
                await _manager.author.CreateAuthor(authorDto);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error("Error Create Author");
                throw new Exception("Error Message:{0}", ex);
            }
            
        }


        [HttpPut("update-author")]
        public async Task<IActionResult> UpdateAuthor(AuthorDto authorDto)
        {
            try
            {
                Log.Information("İnformation Update Author");
                await _manager.author.UpdateAuthor(authorDto);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Update Author");
                throw new Exception("Error Message:{0}", ex);
            }
            
        }


        [HttpPost("delete-author/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                Log.Information("İnformation Delete Author");
                await _manager.author.DeleteAuthor(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Error Delete Author");
                throw new Exception("Error Message:{0}", ex);
            }
            
        }


    }
}
