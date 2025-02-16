using Dastone.Helpers;
using Dastone.Models;
using Dastone.Models.ModelsDto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Dastone.Controllers
{

    public class AuthenticationController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var token = _contextAccessor.HttpContext?.Request.Cookies["LibraryCookie"];
                    var refreshToken = _contextAccessor.HttpContext?.Request.Cookies["RefreshLibraryCookie"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        Response.Cookies.Delete("LibraryCookie", new CookieOptions { HttpOnly = true, Secure = false, SameSite = SameSiteMode.Strict });
                    }
                    if (!string.IsNullOrEmpty(refreshToken))
                    {
                        Response.Cookies.Delete("RefreshLibraryCookie", new CookieOptions { HttpOnly = true, Secure = false, SameSite = SameSiteMode.Strict });
                    }
                    string baseUrl = _configuration["ClientUrl:BaseUrl"];
                    string host = _configuration["ClientUrl:Host"];

                    string LoginUrl = $"{baseUrl}:{host}/Authentication/login";
                    string claimUrl = $"{baseUrl}:{host}/Authentication/get-claims?username={user.UserName}";

                    HttpClient client = new HttpClient();
                    HttpResponseMessage responseMessage = await client.PostAsJsonAsync(LoginUrl, user);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        TokenDto tokenDto = JsonConvert.DeserializeObject<TokenDto>(data);

                        if (tokenDto != null)
                        {
                            Response.Cookies.Append("LibraryCookie", tokenDto.AccessToken, new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = false,
                                SameSite = SameSiteMode.Strict
                            });

                            Response.Cookies.Append("RefreshLibraryCookie", tokenDto.RefreshToken, new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = false,
                                SameSite = SameSiteMode.Strict
                            });
                            var claimsDatas = await client.GetAsync(claimUrl);
                            var claimData = await claimsDatas.Content.ReadFromJsonAsync<List<ClaimDto>>();
                            if (claimData != null)
                            {
                                var authProperties = new AuthenticationProperties
                                {
                                    IsPersistent = true
                                };
                                var claims = claimData.Select((c, index) =>
                                index < 3
                                ? new Claim(c.Type, c.Value)
                                : new Claim(c.Type, c.Value))
                             .ToList();

                                var userIdentity = new ClaimsIdentity(claims, "LibraryCookieScheme");
                                await HttpContext.SignOutAsync("LibraryCookieScheme");
                                await HttpContext.SignInAsync("LibraryCookieScheme", new ClaimsPrincipal(userIdentity), authProperties);
                            }
                            else
                            {
                                ModelState.AddModelError("","Claim verileri alınamadı.");
                                return View("Index");
                            }
                            return RedirectToAction("Index","Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Token alınamadı.");
                            return RedirectToAction("Index", "Authentication");
                        }

                    }
                    else 
                    {
                        ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
                        return RedirectToAction("Index", "Authentication");
                    }

                }
                else 
                {
                    ModelState.AddModelError("", "Model Geçersiz.");
                    return View("Index");
                }
            }
            catch 
            {
                ModelState.AddModelError("", "Giriş sunucu hatası.");
                return RedirectToAction("Index", "Authentication");
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();   
        }
        public async Task<IActionResult> LogOut()
        {
            var token = _contextAccessor.HttpContext?.Request.Cookies["LibraryCookie"];
            if (string.IsNullOrEmpty(token))
            {
                Response.Cookies.Delete("LibraryCookie",new CookieOptions { HttpOnly=true,Secure=false});
                Response.Cookies.Delete("RefreshLibraryCookie", new CookieOptions { HttpOnly = true, Secure = false });
            }
            await HttpContext.SignOutAsync("LibraryCookieScheme");
            return RedirectToAction("Index","Authentication");
        }
        public new IActionResult NotFound() { return View(); }

        public IActionResult InternalError() { return View(); }

        public IActionResult LockScreen() { return View(); }

        public IActionResult RecoverPassword() { return View(); }

        public IActionResult Register() { return View(); }

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
    }
}
