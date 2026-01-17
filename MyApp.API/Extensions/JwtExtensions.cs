using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyApp.Services.Auth.Options;
using System.Text;

namespace MyApp.API.Extensions
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            var jwtSettings = configuration.GetSection(JwtSettings.SectionName);
            var secretKey = jwtSettings["SecretKey"];

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT SecretKey is not configured in appsettings.json");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerOptions>>();
                        logger.LogError(context.Exception,
                            "JWT Authentication Failed. Path: {Path}, Error: {Error}",
                            context.Request.Path, context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerOptions>>();
                        logger.LogInformation(
                            "JWT Token Validated successfully. User: {UserName}, Path: {Path}",
                            context.Principal?.Identity?.Name, context.Request.Path);
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerOptions>>();
                        logger.LogWarning(
                            "JWT Challenge triggered. Path: {Path}, Error: {Error}, ErrorDescription: {ErrorDescription}",
                            context.Request.Path, context.Error, context.ErrorDescription);
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerOptions>>();
                        var token = context.Token;
                        logger.LogDebug(
                            "JWT Token received. Path: {Path}, HasToken: {HasToken}",
                            context.Request.Path, !string.IsNullOrEmpty(token));
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}

