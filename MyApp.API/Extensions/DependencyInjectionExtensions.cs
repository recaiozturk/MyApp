using MyApp.Core.Interfaces;
using MyApp.Data.Repositories;
using MyApp.Services;
using MyApp.Services.Mapping;

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

