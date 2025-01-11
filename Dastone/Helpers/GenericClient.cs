using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.Http.Headers;


namespace Dastone.Helpers
{
    public static class GenericClient
    {
        public static HttpClient Client { get; private set; }
        public static void InitializeClientBaseAdress(this IServiceCollection services,IConfiguration configuration)
        {
            var ApiBaseUrl=configuration.GetSection("ApiBaseUrl").Value;
            Client = new HttpClient();
            Client.BaseAddress = new Uri(ApiBaseUrl);
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
