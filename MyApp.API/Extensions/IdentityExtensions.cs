using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyApp.API.Options;
using MyApp.Data.Shared.Entities;
using MyApp.Data;

namespace MyApp.API.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.SectionName));
            
            services.Configure<SuperAdminSettings>(configuration.GetSection(SuperAdminSettings.SectionName));

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

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<MyAppDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}

