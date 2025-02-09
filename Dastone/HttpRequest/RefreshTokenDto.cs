using Dastone.Models.ModelsDto;

namespace Dastone.HttpRequest
{
    public class RefreshTokenDto
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public RefreshTokenDto(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> Refresh()
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["LibraryCookie"];
            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["RefreshLibraryCookie"];
            var clientRequest = _httpClientFactory.CreateClient("MyClient");
            var re = clientRequest.BaseAddress;
            string urlRef = $"http://{re?.Host}{"api/Authentication/refresh"}";
            TokenDto tokenDto = new TokenDto();
            tokenDto.RefreshToken = refreshToken;
            tokenDto.AccessToken = accessToken;
            var tokenRefresh = await clientRequest.PostAsJsonAsync(urlRef, tokenDto);
            if (tokenRefresh.IsSuccessStatusCode)
            {

                TokenDto newToken = await tokenRefresh.Content.ReadFromJsonAsync<TokenDto>();

                var context = _httpContextAccessor.HttpContext;
                if (context != null)
                {
                    context.Response.Cookies.Delete("LibraryCookie", new CookieOptions { HttpOnly = true, Secure = false, SameSite = SameSiteMode.Strict });
                    context.Response.Cookies.Delete("RefreshLibraryCookie", new CookieOptions { HttpOnly = true, Secure = false, SameSite = SameSiteMode.Strict });
                    if (newToken != null)
                    {
                        context.Response.Cookies.Append("LibraryCookie", newToken.AccessToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = false,
                            SameSite = SameSiteMode.Strict
                        });

                        context.Response.Cookies.Append("RefreshLibraryCookie", newToken.RefreshToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = false,
                            SameSite = SameSiteMode.Strict
                        });
                    }
                }
                return true;
            }
            return false;
        }
    }
}
