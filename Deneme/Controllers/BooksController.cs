using Deneme.Models;
using Deneme.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Deneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BooksController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("create-book")]
        public IActionResult CreateBook(Book books)
        {
            try
            {
                _context.Books.Add(books);
                _context.SaveChanges();
                return Ok(books);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
          
        }
        [HttpGet("get-books")]
        public IActionResult GetAllBooks()
        {
            try
            {
               var books= _context.Books.ToList();
                return Ok(books);
                
            }
            catch (Exception)
            {

               return NotFound();
            }
        }
        [HttpGet("get-book-by-id{Id}")]
        public IActionResult GetOneBook(int Id)
        {
            try
            {
                var book = _context.Books.ToList().Find(w => w.BookId == Id);
                return Ok(book);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [HttpDelete("DeleteOneBook{Id}")]
        public IActionResult DeleteOneBook(int Id)
        {
            try
            {
                var book = _context.Books.ToList().Find(w => w.BookId == Id);
                
                if (book is null) NotFound();

                _context.Books.Remove(book);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [HttpPut("UpdateOneBook{Id}")]
        public IActionResult UpdateOneBook(Book books,int Id)
        {
            try
            {
                var book = _context.Books.ToList().Where(w => w.BookId == Id);

                if (book is null) 
                    NotFound();

                _context.Books.Update(books);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}
