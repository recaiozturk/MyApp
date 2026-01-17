namespace MyApp.API.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddAngularCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    policy => policy
                        .WithOrigins("http://localhost:4200", "https://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            return services;
        }
    }
}

