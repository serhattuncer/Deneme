using Dastone.Helpers;
using Dastone.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using System.Linq;

namespace Dastone.Controllers
{
    public class UserClaimsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            try
            {
                var response = await GenericClient.Client.GetAsync("UserClaims/get-userclaims");

                if (response.IsSuccessStatusCode)
                {
                    var jsonresponse = await response.Content.ReadAsStringAsync();
                    var model = System.Text.Json.JsonSerializer.Deserialize<List<UserClaims>>(jsonresponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View(model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Apiden veri alınamıyor";
                    return View(new List<UserClaims>());
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        [HttpPost]
        public async Task Create([FromBody] UserClaimsDto userClaims)
        {   
            
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(userClaims);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PostAsync("UserClaims/create-userclaim-list", content);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        [HttpPost]
        public async Task Update([FromBody] UserClaims userClaims)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(userClaims);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PutAsync("UserClaims/update-userclaim", content);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        [HttpPost]
        public async Task Delete([FromBody] int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(Id);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    string url = "UserClaims/delete-userclaim";
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
