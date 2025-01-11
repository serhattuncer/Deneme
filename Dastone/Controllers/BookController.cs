using Dastone.Helpers;
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
		[HttpGet]
        public async Task<IActionResult> Index() //Index
            
        { //serhattekstilgururlayerli
		
			try//yorum satırı 
			{
				var response = await GenericClient.Client.GetAsync("Book/get-books");
				if (response.IsSuccessStatusCode)
				{
					var jsonresponse = await response.Content.ReadAsStringAsync();
					var model = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(jsonresponse, new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					});
					return View(model);
				}
				else 
				{
					ViewBag.ErrorMessage = "Apiden veri alınamıyor"; //hi serhat how are you today ?
					return View(new List<Book>());//abcdefghjklmnoprstuvyz
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
			if (ModelState.IsValid) 
			{
				try
				{
					var jsonData=JsonConvert.SerializeObject(book);
					var content=new StringContent(jsonData,Encoding.UTF8,"application/json");
					await GenericClient.Client.PostAsync("Book/create-book",content);
				}
				catch (Exception ex)
				{

					throw;
				}
			}
		}

        [HttpPut]
        public async Task Update([FromBody] Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(book);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PutAsync("Book/update-book", content);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        [HttpDelete]
        public async Task Delete([FromBody] int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(Id);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    string url = " Book/delete-book";
                    string fulurl = $"{url}/{Id}";
                    await GenericClient.Client.PostAsync(fulurl,content);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
