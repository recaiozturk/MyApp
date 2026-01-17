using MyApp.API.Middleware;

namespace MyApp.API.Extensions
{
    public static class ExceptionHandlingExtensions
    {
        public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }

        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler();

            return app;
        }
    }
}



