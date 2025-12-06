using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyApp.API.Options;
using MyApp.Core.Entities;
using MyApp.Data;

namespace MyApp.API.Extensions
{
    public static class IdentityExtensions
    {
        /// <summary>
        /// DbContext ve ASP.NET Core Identity servislerini yapılandırır.
        /// Options Pattern kullanarak appsettings.json'dan Database ayarlarını okur.
        /// </summary>
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Options Pattern ile DatabaseSettings'i register et
            services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.SectionName));
            
            // Options Pattern ile SuperAdminSettings'i register et
            services.Configure<SuperAdminSettings>(configuration.GetSection(SuperAdminSettings.SectionName));

            // Add DbContext - Options'dan connection string'i al
            services.AddDbContext<MyAppDbContext>((serviceProvider, options) =>
            {
                var dbSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                
                if (string.IsNullOrEmpty(dbSettings.DefaultConnection))
                {
                    throw new InvalidOperationException("DefaultConnection is not configured in appsettings.json");
                }

                options.UseSqlServer(
                    dbSettings.DefaultConnection,
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null));
            });

            // Add Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

                // User settings
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<MyAppDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}

