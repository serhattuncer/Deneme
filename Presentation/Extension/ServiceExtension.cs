using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Abstract;
using Services.Concrete;

namespace Presentation.Extension
{
    public static class ServiceExtension
    {
        public static void ConfigurationSQLContext(this IServiceCollection Services , IConfiguration Configuration)
        {
            Services.AddDbContext<RepositoryContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ConnectionString"),b=>b.MigrationsAssembly("Repositories")));
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

            Services.AddScoped<IGenericRepository<Book>,GenericRepository<Book>>();
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

            Services.AddScoped<IServiceManager, ServiceManager>();
        }
    }
}
