using MyApp.Data.Shared.Interfaces;
using MyApp.Data.Shared.Repositories;
using MyApp.Data.Product.Interfaces;
using MyApp.Data.Product.Repositories;
using MyApp.Data.Log.Interfaces;
using MyApp.Data.Log.Repositories;
using MyApp.Services.Product;
using MyApp.Services.Product.Interfaces;
using MyApp.Services.Product.Mapping;
using MyApp.Services.Auth;
using MyApp.Services.Auth.Interfaces;
using MyApp.Services.Log;
using MyApp.Services.Log.Interfaces;

namespace MyApp.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Uygulama servislerini, repository'leri ve AutoMapper'ı DI container'a ekler.
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(typeof(ProductMappingProfile));

            // Add Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ILogRepository, LogRepository>();

            // Add Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILogService, LogService>();

            return services;
        }
    }
}

