using AutoMapper;
using Entities.ModelDto;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Services.Abstract;

namespace Presentation.Controllers
{
    [Route("Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IServiceManager _manager;
       
        public BookController(IServiceManager manager)
        {
            _manager = manager;
        }

        [Authorize(Policy = "GetBooks")]
        [HttpGet("get-books")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                Log.Information("İnformation Get Books");
                var list = _manager.book.GetBooks().Result.Where(w=>w.IsDeleted==false).ToList();
                if (list is null)
                {
                    return NotFound("Veri Bulunamadı.");
                }
             
                

                return Ok(list);

            }
            catch (Exception ex)
            {
                Log.Error("Error Get Books");
                throw new Exception("Error Message:{0}", ex);
            }

        }

        [Authorize(Policy = "GetBookById")]
        [HttpGet("get-book-by-id{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                Log.Information("İnformation Get BookById");
                var result = await _manager.book.GetBookById(id);
                if (result is null)
                {
                    return NotFound(id + "Numaralı Id'ye ait veri bulunamadı.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error Get BookById");
                throw new Exception("Error Message:{0}", ex); ;
            }
            
        }

        [Authorize(Policy = "CreateBook")]
        [HttpPost("create-book")]
        public async Task<IActionResult> CreateBook(BookDto bookDto)
        {
            try
            {
                Log.Information("İnformation Create Book");
                await _manager.book.CreateBook(bookDto);
                return Ok();
            }
            catch (Exception ex)
            {

                Log.Error("Error Create Book");
                throw new Exception("Error Message:{0}", ex); 
            }
            
        }

        [Authorize(Policy = "UpdateBook")]
        [HttpPut("update-book")]
        public async Task<IActionResult> Updatebook(BookDto bookDto)
        {
            try
            {
                Log.Information("İnformation Update Book");
                await _manager.book.UpdateBook(bookDto);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Update Book");
                throw new Exception("Error Message:{0}", ex);
            }
            
        }

        [Authorize(Policy = "DeleteBook")]
        [HttpPost("delete-book/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                Log.Information("İnformation Delete Book");
                await _manager.book.DeleteBook(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                Log.Error("Error Delete Book");
                throw new Exception("Error Message:{0}", ex);
            }
            
        }


    }
}
