using Dastone.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using Dastone.Models;

namespace Dastone.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            try
            {
                var response = await GenericClient.Client.GetAsync("User/get-users");
                if (response.IsSuccessStatusCode)
                {
                    var jsonresponse = await response.Content.ReadAsStringAsync();
                    var model = System.Text.Json.JsonSerializer.Deserialize<List<Users>>(jsonresponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View(model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Apiden veri alınamıyor";
                    return View(new List<Users>());
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        [HttpPost]
        public async Task Create([FromBody] Users users)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(users);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PostAsync("User/create-user", content);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        [HttpPut]
        public async Task Update([FromBody] Users users)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(users);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PutAsync("User/update-user", content);
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
                    string url = " User/delete-user";
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
