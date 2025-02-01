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

        [HttpPost]
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
        [HttpPost]
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

        [HttpPost]
        public async Task<JsonResult> GetUserClaimsById(int id)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(id);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                string url = "UserClaims/get-user-claims-by-userid";
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
        public async Task<JsonResult> GetUserRolesById(int id)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(id);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                string url = "UserRoles/get-user-roles-by-userroleid";
                string fulurl = $"{url}/{id}";
                var response = await GenericClient.Client.PostAsync(fulurl, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonresponse = await response.Content.ReadAsStringAsync();
                    var model = System.Text.Json.JsonSerializer.Deserialize<List<Roles>>(jsonresponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    List<int> rolesIds = model.Select(s => s.Role_Id).ToList();
                    return Json(rolesIds);
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
        public async Task<JsonResult> GetRoles()
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
                    return Json(model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Apiden veri alınamıyor";
                    return Json(new List<Roles>());
                }
            }
            catch (Exception ex)
            {

                return Json("Error");
            }
        }
    }
}
