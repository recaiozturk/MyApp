using MyApp.API.Extensions;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog'u yapılandır
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerConfiguration();
    builder.Services.AddGlobalExceptionHandler();// Global Exception Handling
    builder.Services.AddIdentityServices(builder.Configuration);// Add DbContext and Identity
    builder.Services.AddJwtAuthentication(builder.Configuration);// Add JWT Authentication
    builder.Services.AddApplicationServices();// Add Application Services (Repositories, Services, AutoMapper)
    builder.Services.AddAngularCors(builder.Configuration);// Add CORS

    var app = builder.Build();

    // CORS - Authentication'dan ÖNCE olmalı (preflight request'ler için)
    if (app.Environment.IsDevelopment())
    {
        app.UseCors("AllowAngularApp");
    }

    app.UseHttpsRedirection();
    app.UseGlobalExceptionHandler();// Global Exception Handler Middleware

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.MapFallbackToFile("index.html");
    }

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    await app.SeedSuperAdminAsync();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}
