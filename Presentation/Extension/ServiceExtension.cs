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
            Services.AddScoped<IRepositoryManager, RepositoryManager>();
            Services.AddScoped<IGenericRepository<Book>,GenericRepository<Book>>();
            Services.AddScoped<IGenericRepository<Author>, GenericRepository<Author>>();
            Services.AddScoped<IGenericRepository<PublishingHouse>, GenericRepository<PublishingHouse>>();
        }
        public static void ConfiguerServiceManager(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceBook, ServiceBook>();
            Services.AddScoped<IAuthorService, AuthorService>();
            Services.AddScoped<IPublishingHouseService, PublishingHouseService>();
            Services.AddScoped<IServiceManager, ServiceManager>();
        }
    }
}
