using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;
using MyApp.Data;

namespace MyApp.API.Extensions
{
    public static class DatabaseSeedExtensions
    {
        /// <summary>
        /// Veritabanı oluşturulduğunda SuperAdmin kullanıcısını ve rolünü seed eder.
        /// Eğer kullanıcı zaten varsa, işlem yapılmaz.
        /// </summary>
        public static async Task SeedSuperAdminAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                // Veritabanının hazır olduğundan emin ol
                var dbContext = scope.ServiceProvider.GetRequiredService<MyAppDbContext>();
                await dbContext.Database.MigrateAsync();

                // SuperAdmin rolünü oluştur (yoksa)
                const string superAdminRole = "SuperAdmin";
                if (!await roleManager.RoleExistsAsync(superAdminRole))
                {
                    await roleManager.CreateAsync(new IdentityRole(superAdminRole));
                    logger.LogInformation("SuperAdmin role created.");
                }

                // SuperAdmin kullanıcısını oluştur (yoksa)
                var superAdminConfig = configuration.GetSection("SuperAdmin");
                var userName = superAdminConfig["UserName"];
                var email = superAdminConfig["Email"];
                var password = superAdminConfig["Password"];
                var firstName = superAdminConfig["FirstName"];
                var lastName = superAdminConfig["LastName"];

                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    logger.LogWarning("SuperAdmin configuration is missing in appsettings.json. Skipping seed.");
                    return;
                }

                var existingUser = await userManager.FindByNameAsync(userName);
                if (existingUser == null)
                {
                    var superAdmin = new ApplicationUser
                    {
                        UserName = userName,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true,
                        EmailConfirmed = true // SuperAdmin için email confirmation atla
                    };

                    var result = await userManager.CreateAsync(superAdmin, password);
                    if (result.Succeeded)
                    {
                        // SuperAdmin rolünü ata
                        await userManager.AddToRoleAsync(superAdmin, superAdminRole);
                        logger.LogInformation("SuperAdmin user created successfully. Username: {UserName}, Email: {Email}", userName, email);
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

