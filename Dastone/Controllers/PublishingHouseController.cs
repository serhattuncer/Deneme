using Dastone.Helpers;
using Dastone.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Dastone.Controllers
{
    public class PublishingHouseController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await GenericClient.Client.GetAsync("PublishingHouse/get-publishinghouses");
                if (response.IsSuccessStatusCode)
                {
                    var jsonresponse = await response.Content.ReadAsStringAsync();
                    var model = System.Text.Json.JsonSerializer.Deserialize<List<PublishingHouse>>(jsonresponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View(model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Apiden veri alınamıyor";
                    return View(new List<PublishingHouse>());
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        [HttpPost]
        public async Task Create([FromBody] PublishingHouse publishingHouse )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(publishingHouse);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PostAsync("PublishingHouse/create-publishinghouse", content);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        [HttpPut]
        public async Task Update([FromBody] PublishingHouse publishingHouse)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(publishingHouse);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PutAsync("PublishingHouse/update-publishinghouse", content);
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
                    string url = " PublishingHouse/delete-publishinghouse";
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
