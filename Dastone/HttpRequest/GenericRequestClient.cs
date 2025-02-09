using Newtonsoft.Json;
using System.Text;
using System;

namespace Dastone.HttpRequest
{
    public class GenericRequestsClient<T>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        
        public GenericRequestsClient(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ResponseWrapper<T>> GetHttpRequest(string url)
        {
            try
            {
                RefreshTokenDto refreshTokenDto = new RefreshTokenDto(_httpContextAccessor, _httpClientFactory);
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["LibraryCookie"];

                if (!string.IsNullOrEmpty(token))
                {
                    var clientRequest = _httpClientFactory.CreateClient("MyClient");
                    if (clientRequest != null)
                    {
                        clientRequest.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                        string BaseUrl = _configuration["ClientUrl:BaseUrl"];
                        string Host = _configuration["ClientUrl:Host"];
                        

                        string fullUrl = $"{BaseUrl} : {Host}/{url}";

                        HttpResponseMessage response = await clientRequest.GetAsync(fullUrl);

                        switch ((int)response.StatusCode)
                        {
                            case 401:
                                int retryCount = 0;
                                const int maxRetries = 3;
                                while (retryCount < maxRetries)
                                {
                                    bool refreshed = await refreshTokenDto.Refresh();
                                    if (refreshed)
                                    {
                                        return await GetHttpRequest(url);
                                    }
                                    retryCount++;
                                }
                                return new ResponseWrapper<T>(false, default, "Unauthorized", 401);

                            case 200:
                                T data = await response.Content.ReadFromJsonAsync<T>();
                                return new ResponseWrapper<T>(true, data, null, 200);

                            case 404:
                                return new ResponseWrapper<T>(false, default, "Not Found", 404);
                            case 403:
                                return new ResponseWrapper<T>(false, default, "403 Forbidden", 403);

                            default:
                                return new ResponseWrapper<T>(false, default, "An error occurred", (int)response.StatusCode);
                        }
                    }
                }
                return new ResponseWrapper<T>(false, default, "Token not found", 401);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<T>(false, default, ex.Message, 500);
            }
        }

        public async Task<ResponseWrapper<List<T>>> GetListHttpRequest(string url)
        {
            try
            {
                RefreshTokenDto refreshTokenDto = new RefreshTokenDto(_httpContextAccessor, _httpClientFactory);
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["LibraryCookie"];

                if (!string.IsNullOrEmpty(token))
                {
                    var clientRequest = _httpClientFactory.CreateClient("MyClient");
                    if (clientRequest != null)
                    {
                        clientRequest.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                        string BaseUrl = _configuration["ClientUrl:BaseUrl"];
                        string Host = _configuration["ClientUrl:Host"];
                       

                        string fullUrl = $"{BaseUrl}:{Host}/{url}";

                        HttpResponseMessage response = await clientRequest.GetAsync(fullUrl);

                        switch ((int)response.StatusCode)
                        {
                            case 401:
                                int retryCount = 0;
                                const int maxRetries = 3;
                                while (retryCount < maxRetries)
                                {
                                    bool refreshed = await refreshTokenDto.Refresh();
                                    if (refreshed)
                                    {
                                        return await GetListHttpRequest(url);
                                    }
                                    retryCount++;
                                }
                                return new ResponseWrapper<List<T>>(false, null, "Unauthorized", 401);

                            case 200:
                                List<T> data = await response.Content.ReadFromJsonAsync<List<T>>();
                                return new ResponseWrapper<List<T>>(true, data, null, 200);

                            case 404:
                                return new ResponseWrapper<List<T>>(false, null, "Not Found", 404);
                            case 403:
                                return new ResponseWrapper<List<T>>(false, null, "403 Forbidden", 403);

                            default:
                                return new ResponseWrapper<List<T>>(false, null, "An error occurred", (int)response.StatusCode);
                        }
                    }
                }
                return new ResponseWrapper<List<T>>(false, null, "Token not found", 401);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<List<T>>(false, null, ex.Message, 500);
            }
        }


        public async Task<ResponseWrapper<string>> PostRequestGeneric(string url, T entity)
        {
            try
            {
                if (ContainsScript(entity))
                    return new ResponseWrapper<string>(false, null, "Hata", 400);

                RefreshTokenDto refreshTokenDto = new RefreshTokenDto(_httpContextAccessor, _httpClientFactory);

                var jsonData = JsonConvert.SerializeObject(entity);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var token = _httpContextAccessor.HttpContext?.Request.Cookies["LibraryCookie"];
                if (!string.IsNullOrEmpty(token))
                {
                    var clientRequest = _httpClientFactory.CreateClient("MyClient");
                    if (clientRequest != null)
                    {
                        clientRequest.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                        string BaseUrl = _configuration["ClientUrl:BaseUrl"];
                        string Host = _configuration["ClientUrl:Host"];

                        string fullUrl = $"{BaseUrl}:{Host}/{url}";

                        var response = await clientRequest.PostAsync(fullUrl, content);

                        switch ((int)response.StatusCode)
                        {
                            case 200:
                                return new ResponseWrapper<string>(true, "Başarılı", null, 200);

                            case 401:
                                int retryCount = 0;
                                const int maxRetries = 3;
                                while (retryCount < maxRetries)
                                {
                                    bool refreshed = await refreshTokenDto.Refresh();
                                    if (refreshed)
                                    {
                                        return await PostRequestGeneric(url, entity);
                                    }
                                    retryCount++;
                                }
                                return new ResponseWrapper<string>(false, null, "Yetkilendirme hatası", 401);

                            default:
                                return new ResponseWrapper<string>(false, null, $"Hata: {response.StatusCode}", (int)response.StatusCode);
                        }
                    }
                }
                return new ResponseWrapper<string>(false, null, "Token not found", 401);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<string>(false, null, ex.Message, 500);
            }
        }



        public async Task<ResponseWrapper<string>> UpdateRequestGeneric(string url, T entity)
        {
            try
            {
                if (ContainsScript(entity))
                    return new ResponseWrapper<string>(false, null, "Hata", 400);

                RefreshTokenDto refreshTokenDto = new RefreshTokenDto(_httpContextAccessor, _httpClientFactory);

                var token = _httpContextAccessor.HttpContext?.Request.Cookies["LibraryCookie"];
                if (!string.IsNullOrEmpty(token))
                {
                    var clientRequest = _httpClientFactory.CreateClient("MyClient");
                    if (clientRequest != null)
                    {
                        clientRequest.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                        var jsonData = JsonConvert.SerializeObject(entity);
                        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                        string BaseUrl = _configuration["ClientUrl:BaseUrl"];
                        string Host = _configuration["ClientUrl:Host"];
                        

                        string fullUrl = $"{BaseUrl} : {Host}/{url}";

                        var response = await clientRequest.PostAsync(fullUrl, content);

                        switch ((int)response.StatusCode)
                        {
                            case 200:
                                return new ResponseWrapper<string>(true, "Güncelleme başarılı", null, 200);

                            case 401:
                                int retryCount = 0;
                                const int maxRetries = 3;
                                while (retryCount < maxRetries)
                                {
                                    bool refreshed = await refreshTokenDto.Refresh();
                                    if (refreshed)
                                    {
                                        return await UpdateRequestGeneric(url, entity);
                                    }
                                    retryCount++;
                                }
                                return new ResponseWrapper<string>(false, null, "Yetkilendirme hatası", 401);

                            case 404:
                                return new ResponseWrapper<string>(false, null, "Kaynak bulunamadı", 404);

                            default:
                                return new ResponseWrapper<string>(false, null, $"Hata: {response.StatusCode}", (int)response.StatusCode);
                        }
                    }
                }
                return new ResponseWrapper<string>(false, null, "Token not found", 401);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<string>(false, null, ex.Message, 500);
            }
        }

        public async Task<ResponseWrapper<T>> GetByIdGeneric(string url, int id)
        {
            try
            {
                RefreshTokenDto refreshTokenDto = new RefreshTokenDto(_httpContextAccessor, _httpClientFactory);
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["LibraryCookie"];

                if (!string.IsNullOrEmpty(token))
                {
                    var clientRequest = _httpClientFactory.CreateClient("MyClient");

                    string BaseUrl = _configuration["ClientUrl:BaseUrl"];
                    string Host = _configuration["ClientUrl:Host"];
                    

                    string fullUrl = $"{BaseUrl}:{Host}/{url}?id={id}";

                    clientRequest.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    HttpResponseMessage response = await clientRequest.GetAsync(fullUrl);

                    switch ((int)response.StatusCode)
                    {
                        case 200:
                            var data = await response.Content.ReadFromJsonAsync<T>();
                            return new ResponseWrapper<T>(true, data, null, 200);
                        case 401:
                            int retryCount = 0;
                            const int maxRetries = 3;
                            while (retryCount < maxRetries)
                            {
                                bool again = await refreshTokenDto.Refresh();
                                if (again)
                                {
                                    return await GetByIdGeneric(url, id);
                                }
                                retryCount++;
                            }
                            return new ResponseWrapper<T>(false, default, "Yetkilendirme hatası", 401);
                        default:
                            return new ResponseWrapper<T>(false, default, $"Hata: {response.StatusCode}", (int)response.StatusCode);
                    }
                }
                else return new ResponseWrapper<T>(false, default, "Token not found", 401);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<T>(false, default, ex.Message, 500);
            }
        }
        public async Task<ResponseWrapper<string>> DeleteRequestGeneric(string url, int id)
        {
            try
            {
                RefreshTokenDto refreshTokenDto = new RefreshTokenDto(_httpContextAccessor, _httpClientFactory);
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["LibraryCookie"];

                if (!string.IsNullOrEmpty(token))
                {
                    var clientRequest = _httpClientFactory.CreateClient("MyClient");

                    string BaseUrl = _configuration["ClientUrl:BaseUrl"];
                    string Host = _configuration["ClientUrl:Host"];

                    string fullUrl = $"{BaseUrl}:{Host}/{url}{id}";

                    clientRequest.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    var response = await clientRequest.DeleteAsync(fullUrl);

                    switch ((int)response.StatusCode)
                    {
                        case 200:
                            return new ResponseWrapper<string>(true, "Silme işlemi başarılı", null, 200);
                        case 401:
                            int retryCount = 0;
                            const int maxRetries = 3;
                            while (retryCount < maxRetries)
                            {
                                bool again = await refreshTokenDto.Refresh();
                                if (again)
                                {
                                    return await DeleteRequestGeneric(url, id);
                                }
                                retryCount++;
                            }
                            return new ResponseWrapper<string>(false, null, "Yetkilendirme hatası", 401);
                        case 404:
                            return new ResponseWrapper<string>(false, null, "Kaynak bulunamadı", 404);
                        default:
                            return new ResponseWrapper<string>(false, null, $"Hata: {response.StatusCode}", (int)response.StatusCode);
                    }
                }
                else return new ResponseWrapper<string>(false, null, "Token not found", 401);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<string>(false, null, ex.Message, 500);
            }
        }
        private bool ContainsScript(T entity)
        {
            var serializedEntity = JsonConvert.SerializeObject(entity);

            string[] dangerousKeywords = {
              "<script>", "javascript:", "onerror=", "onload=",
              "onmouseover=", "onfocus=", "onblur=", "onclick=",
              "onkeypress=", "onkeydown=", "onkeyup=",
              "eval(", "expression(", "iframe", "frame", "object",
              "embed", "applet", "base64,", "src=", "javascript:",
              "vbscript:", "data:text/html;base64,", "url()",
              "@import", "@media", "<iframe>", "alert(", "console.log(",
              "document.cookie", "document.location", "window.location",
              "window.onload=", "position: absolute;", "z-index:",
              "expression", "background:url"
          };

            foreach (var keyword in dangerousKeywords)
            {
                if (serializedEntity.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
