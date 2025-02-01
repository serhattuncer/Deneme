using Dastone.Helpers;
using Dastone.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;

namespace Dastone.Controllers
{
    public class RoleController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            try
            {
                var response = await GenericClient.Client.GetAsync("Role/get-roles");
                if (response.IsSuccessStatusCode)
                {
                    var jsonresponse = await response.Content.ReadAsStringAsync();
                    var model = System.Text.Json.JsonSerializer.Deserialize<List<Roles>>(jsonresponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View(model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Apiden veri alınamıyor";
                    return View(new List<Roles>());
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        public async Task<JsonResult> GetRoleClaimsById(int id)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(id);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                string url = "RoleClaims/get-role-claims-by-roleid";
                string fulurl = $"{url}/{id}";
                var response = await GenericClient.Client.PostAsync(fulurl, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonresponse = await response.Content.ReadAsStringAsync();
                    var model = System.Text.Json.JsonSerializer.Deserialize<List<Claims>>(jsonresponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    List<int> claimsIds = model.Select(s => s.Claims_Id).ToList();
                    return Json(claimsIds);
                }
                else
                {
                    ViewBag.ErrorMessage = "Apiden veri alınamıyor";
                    return Json(new List<Claims>());
                }
            }
            catch (Exception ex)
            {

                return Json("Error");
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetClaims()
        {

            try
            {
                var response = await GenericClient.Client.GetAsync("Claim/get-claims");
                if (response.IsSuccessStatusCode)
                {
                    var jsonresponse = await response.Content.ReadAsStringAsync();
                    var model = System.Text.Json.JsonSerializer.Deserialize<List<Claims>>(jsonresponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return Json(model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Apiden veri alınamıyor";
                    return Json(new List<Claims>());
                }
            }
            catch (Exception ex)
            {

                return Json("Error");
            }
        }
        [HttpPost]
        public async Task Create([FromBody] Roles roles)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(roles);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PostAsync("Role/create-role", content);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        [HttpPost]
        public async Task Update([FromBody] Roles roles)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(roles);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PutAsync("Role/update-role", content);
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
                    string url = " Role/delete-role";
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
