using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyApp.API.Options;
using MyApp.Data.Shared.Entities;
using MyApp.Data;

namespace MyApp.API.Extensions
{
    public static class DatabaseSeedExtensions
    {
        public static async Task SeedSuperAdminAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var superAdminSettings = scope.ServiceProvider.GetRequiredService<IOptions<SuperAdminSettings>>().Value;
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MyAppDbContext>();
                await dbContext.Database.MigrateAsync();

                const string superAdminRole = "SuperAdmin";
                if (!await roleManager.RoleExistsAsync(superAdminRole))
                {
                    await roleManager.CreateAsync(new IdentityRole(superAdminRole));
                    logger.LogInformation("SuperAdmin role created.");
                }

                if (string.IsNullOrEmpty(superAdminSettings.UserName) || 
                    string.IsNullOrEmpty(superAdminSettings.Email) || 
                    string.IsNullOrEmpty(superAdminSettings.Password))
                {
                    logger.LogWarning("SuperAdmin configuration is missing in appsettings.json. Skipping seed.");
                    return;
                }

                var existingUser = await userManager.FindByNameAsync(superAdminSettings.UserName);
                if (existingUser == null)
                {
                    var superAdmin = new ApplicationUser
                    {
                        UserName = superAdminSettings.UserName,
                        Email = superAdminSettings.Email,
                        FirstName = superAdminSettings.FirstName,
                        LastName = superAdminSettings.LastName,
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(superAdmin, superAdminSettings.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(superAdmin, superAdminRole);
                        logger.LogInformation("SuperAdmin user created successfully. Username: {UserName}, Email: {Email}", 
                            superAdminSettings.UserName, superAdminSettings.Email);
                    }
                    else
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        logger.LogError("Failed to create SuperAdmin user: {Errors}", errors);
                    }
                }
                else
                {
                    logger.LogInformation("SuperAdmin user already exists. Skipping seed.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding SuperAdmin user.");
            }
        }
    }
}

