using EcommercePOC.DataAccess;
using EcommercePOC.Repository;
using EcommercePOC.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace EcommercePOC.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config) 
        {
            //Adding Database
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("EcommerceDBConnection")));

            //Adding Repositories
            services.AddScoped<IAccountRepository, AccountRepositor>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            
            return services;
        }
    }
}
