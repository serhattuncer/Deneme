using Dastone.Helpers;
using Dastone.HttpRequest;
using Dastone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Dastone.Controllers
{
    public class BookController : Controller
    {
        private readonly GenericRequestsClient<Book> _client;

        public BookController(GenericRequestsClient<Book> client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Index()     
        { 
			try 
			{
                var response = await _client.GetHttpRequest("Book/get-books");
				if (response.IsSuccess)
				{
					return View(response);
				}
				else 
				{
					ViewBag.ErrorMessage = "Apiden veri alınamıyor"; 
					return View(new List<Book>());
				}
			}
			catch (Exception ex)
			{

				return View("Error");
			}
        }
		[HttpPost]
		public async Task Create([FromBody]Book book)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _client.PostRequestGeneric("Book/create-book",book);
                }
            }
            catch (Exception)
            {

                throw;
            }
			
		}

        [HttpPost]
        public async Task Update([FromBody] Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _client.UpdateRequestGeneric("Book/update-book",book);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task Delete([FromBody] int Id)
        {
            try
            {
                await _client.DeleteRequestGeneric("Book/delete-book/",Id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
