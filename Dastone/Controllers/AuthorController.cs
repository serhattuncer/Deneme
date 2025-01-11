using Dastone.Helpers;
using Dastone.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Dastone.Controllers
{
    public class AuthorController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await GenericClient.Client.GetAsync("Author/get-authors");
                if (response.IsSuccessStatusCode)
                {
                    var jsonresponse = await response.Content.ReadAsStringAsync();
                    var model = System.Text.Json.JsonSerializer.Deserialize<List<Author>>(jsonresponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View(model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Apiden veri alınamıyor";
                    return View(new List<Author>());
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        [HttpPost]
        public async Task Create([FromBody] Author author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(author);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PostAsync("Author/create-author", content);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        [HttpPut]
        public async Task Update([FromBody] Author author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(author);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PutAsync("Author/update-author", content);
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
                    string url = " Author/delete-author";
                    string fulurl = $"{url}/{Id}";
                    await GenericClient.Client.PostAsync(fulurl, content);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
