using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Dastone.Extension
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("GetBooks", policy =>
               policy.RequireClaim("Book", "GetBooks").RequireAuthenticatedUser());

                options.AddPolicy("GetBookById", policy =>
                policy.RequireClaim("Book", "GetBookById").RequireAuthenticatedUser());

                options.AddPolicy("CreateBook", policy =>
                policy.RequireClaim("Book", "CreateBook").RequireAuthenticatedUser());

                options.AddPolicy("UpdateBook", policy =>
                policy.RequireClaim("Book", "UpdateBook").RequireAuthenticatedUser());

                options.AddPolicy("DeleteBook", policy =>
                policy.RequireClaim("Book", "DeleteBook").RequireAuthenticatedUser());

                //Yazar ile ilgili yetkiler
                options.AddPolicy("GetAuthors", policy =>
                policy.RequireClaim("Author", "GetAuthors").RequireAuthenticatedUser());

                options.AddPolicy("GetAuthorById", policy =>
                policy.RequireClaim("Author", "GetAuthorById").RequireAuthenticatedUser());

                options.AddPolicy("CreateAuthor", policy =>
                policy.RequireClaim("Author", "CreateAuthor").RequireAuthenticatedUser());

                options.AddPolicy("UpdateAuthor", policy =>
                policy.RequireClaim("Author", "UpdateAuthor").RequireAuthenticatedUser());

                options.AddPolicy("DeleteAuthor", policy =>
                policy.RequireClaim("Author", "DeleteAuthor").RequireAuthenticatedUser());

                //Yayınevi ile ilgili yetkiler
                options.AddPolicy("GetPublishingHouses", policy =>
                policy.RequireClaim("PublishingHouse", "GetPublishingHouses").RequireAuthenticatedUser());

                options.AddPolicy("GetPublishingHouseById", policy =>
                policy.RequireClaim("PublishingHouse", "GetPublishingHouseById").RequireAuthenticatedUser());

                options.AddPolicy("CreatePublishingHouse", policy =>
                policy.RequireClaim("PublishingHosue", "CreatePublishingHouse").RequireAuthenticatedUser());

                options.AddPolicy("UpdatePublishingHouse", policy =>
                policy.RequireClaim("PublishingHosue", "UpdatePublishingHouse").RequireAuthenticatedUser());

                options.AddPolicy("DeleteAuthor", policy =>
                policy.RequireClaim("PublishingHosue", "DeletePublishingHouse").RequireAuthenticatedUser());

                //Kullanıcı ile ilgili yetkiler
                options.AddPolicy("GetUsers", policy =>
                policy.RequireClaim("User", "GetUsers").RequireAuthenticatedUser());

                options.AddPolicy("GetUserById", policy =>
                policy.RequireClaim("User", "GetUserById").RequireAuthenticatedUser());

                options.AddPolicy("CreateUser", policy =>
                policy.RequireClaim("User", "CreateUser").RequireAuthenticatedUser());

                options.AddPolicy("UpdateUser", policy =>
                policy.RequireClaim("User", "UpdateUser").RequireAuthenticatedUser());

                options.AddPolicy("DeleteUser", policy =>
                policy.RequireClaim("User", "DeleteUser").RequireAuthenticatedUser());


                //Rol ile ilgili yetkiler
                options.AddPolicy("GetRoles", policy =>
                policy.RequireClaim("Role", "GetRoles").RequireAuthenticatedUser());

                options.AddPolicy("GetRoleById", policy =>
                policy.RequireClaim("Role", "GetRoleById").RequireAuthenticatedUser());

                options.AddPolicy("CreateRole", policy =>
                policy.RequireClaim("Role", "CreateRole").RequireAuthenticatedUser());

                options.AddPolicy("UpdateRole", policy =>
                policy.RequireClaim("Role", "UpdateRole").RequireAuthenticatedUser());

                options.AddPolicy("DeleteRole", policy =>
                policy.RequireClaim("Role", "DeleteRole").RequireAuthenticatedUser());

                //Yetki ile ilgili yetkiler
                options.AddPolicy("GetClaims", policy =>
                policy.RequireClaim("Claim", "GetClaims").RequireAuthenticatedUser());

                options.AddPolicy("GetClaimById", policy =>
                policy.RequireClaim("Claim", "GetClaimById").RequireAuthenticatedUser());

                options.AddPolicy("CreateClaim", policy =>
                policy.RequireClaim("Claim", "CreateClaim").RequireAuthenticatedUser());

                options.AddPolicy("UpdateClaim", policy =>
                policy.RequireClaim("Claim", "UpdateClaim").RequireAuthenticatedUser());

                options.AddPolicy("DeleteClaim", policy =>
                policy.RequireClaim("Claim", "DeleteClaim").RequireAuthenticatedUser());

                //Home ile ilgili yetkiler
                options.AddPolicy("Read", policy =>
               policy.RequireClaim("Home", "Read").RequireAuthenticatedUser());

            });

            return services;
        }
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtSettings = configuration.GetSection("JwtSettings"); //appsettingsteki istenilen tagı okumaya yarar

            var decryptedsecretKey =jwtSettings["SecretKey"];
            var decryptedValidateIssue = jwtSettings["ValidateIssue"];
            var decryptedValidateAudience = jwtSettings["ValidateAudience"];

            var secretKey = jwtSettings["SecretKey"];
            //Tokenlarda hassas veriler hiçbir şekilde yer almamalıdır //userName, password, eMail, phoneNumber etc
            services.AddAuthentication("LibraryCookieScheme")
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = decryptedValidateIssue,
                    ValidAudience = decryptedValidateAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(decryptedsecretKey)),
                    ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(jwtSettings.GetSection("Expire").Value))
                }
                ).AddCookie("LibraryCookieScheme", opt =>
                {
                    opt.Cookie.Name = "LibraryCookie";
                    opt.LoginPath = "/Authentication/Login";
                    opt.LogoutPath = "/Authentication/Logout";
                    opt.AccessDeniedPath = "/Authentication/Login";
                });

            return services;
        }
        public static IServiceCollection AddCustomSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            return services;
        }
        

       
        public static IServiceCollection AddCustomHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("MyClient", client =>
            {
                var baseUrl = configuration.GetValue<string>("ClientUrl:BaseUrl");
               
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddHttpClient(); // Diğer genel HttpClient kaydı

            return services;
        }
    }
}
