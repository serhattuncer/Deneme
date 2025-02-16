using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Abstract;
using Services.Concrete;
using System.Text;
using System;

namespace Presentation.Extension
{
    public static class ServiceExtension
    {
        public static void ConfigurationSQLContext(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<RepositoryContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ConnectionString"), b => b.MigrationsAssembly("Repositories")));
        }
        public static void ConfiguerRepositoryManager(this IServiceCollection Services)
        {

            Services.AddScoped<IRepositoryBook, RepositoryBook>();
            Services.AddScoped<IRepositoryAuthor, RepositoryAuthor>();
            Services.AddScoped<IRepositoryPublishingHouse, RepositoryPublishingHouse>();
            Services.AddScoped<IRepositoryUsers, RepositoryUsers>();
            Services.AddScoped<IRepositoryRoles, RepositoryRoles>();
            Services.AddScoped<IRepositoryClaims, RepositoryClaims>();
            Services.AddScoped<IRepositoryUserClaims, RepositoryUserClaims>();
            Services.AddScoped<IRepositoryRoleClaims, RepositoryRoleClaims>();
            Services.AddScoped<IRepositoryUserRoles, RepositoryUserRoles>();

            Services.AddScoped<IRepositoryManager, RepositoryManager>();

            Services.AddScoped<IGenericRepository<Book>, GenericRepository<Book>>();
            Services.AddScoped<IGenericRepository<Author>, GenericRepository<Author>>();
            Services.AddScoped<IGenericRepository<PublishingHouse>, GenericRepository<PublishingHouse>>();
            Services.AddScoped<IGenericRepository<Users>, GenericRepository<Users>>();
            Services.AddScoped<IGenericRepository<Roles>, GenericRepository<Roles>>();
            Services.AddScoped<IGenericRepository<Claims>, GenericRepository<Claims>>();
            Services.AddScoped<IGenericRepository<UserClaims>, GenericRepository<UserClaims>>();
            Services.AddScoped<IGenericRepository<RoleClaims>, GenericRepository<RoleClaims>>();
            Services.AddScoped<IGenericRepository<UserRoles>, GenericRepository<UserRoles>>();
        }
        public static void ConfiguerServiceManager(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceBook, ServiceBook>();
            Services.AddScoped<IAuthorService, AuthorService>();
            Services.AddScoped<IPublishingHouseService, PublishingHouseService>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<IRoleService, RoleService>();
            Services.AddScoped<IClaimService, ClaimService>();
            Services.AddScoped<IUserClaimsService, UserClaimsService>();
            Services.AddScoped<IRoleClaimsService, RoleClaimsService>();
            Services.AddScoped<IUserRolesService, UserRolesService>();
            Services.AddScoped<IAuthenticationService, AuthenticationService>();

            Services.AddScoped<IServiceManager, ServiceManager>();
        }
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //Kitap ile ilgili yetkiller
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

                //Kullanıcı Yetkileri ile ilgili yetkiler
                options.AddPolicy("GetUserClaims", policy =>
                policy.RequireClaim("UserClaims", "GetUserClaims").RequireAuthenticatedUser());
                options.AddPolicy("CreateUserClaims", policy =>
                policy.RequireClaim("UserClaims", "CreateUserClaims").RequireAuthenticatedUser());
                options.AddPolicy("DeleteUserClaims", policy =>
                policy.RequireClaim("UserClaims", "DeleteUserClaims").RequireAuthenticatedUser());

                //Rol Yetkileri ile ilgili yetkiler
                options.AddPolicy("GetRoleClaims", policy =>
                policy.RequireClaim("RoleClaims", "GetRoleClaims").RequireAuthenticatedUser());
                options.AddPolicy("CreateRoleClaims", policy =>
                policy.RequireClaim("RoleClaims", "CreateRoleClaims").RequireAuthenticatedUser());
                options.AddPolicy("DeleteRoleClaims", policy =>
                policy.RequireClaim("RoleClaims", "DeleteRoleClaims").RequireAuthenticatedUser());

                //Kullanıcı Rol ile ilgili yetkiler
                options.AddPolicy("GetUserRoles", policy =>
                policy.RequireClaim("UserRoles", "GetUserRoles").RequireAuthenticatedUser());
                options.AddPolicy("CreateUserRoles", policy =>
                policy.RequireClaim("UserRoles", "CreateUserRoles").RequireAuthenticatedUser());
                options.AddPolicy("DeleteUserRoles", policy =>
                policy.RequireClaim("UserRoles", "DeleteUserRoles").RequireAuthenticatedUser());

            });
            return services;
        }
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtSettings = configuration.GetSection("JwtSettings"); //appsettingsteki istenilen tagı okumaya yarar

            var decryptedsecretKey = jwtSettings["secretKey"];
            var decryptedValidateIssue = jwtSettings["ValidateIssue"];
            var decryptedValidateAudience = jwtSettings["ValidateAudience"];

            var secretKey = jwtSettings["secretKey"];
            //Tokenlarda hassas veriler hiçbir şekilde yer almamalıdır //userName, password, eMail, phoneNumber etc
            services.AddAuthentication("InventoryCookieScheme")
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
