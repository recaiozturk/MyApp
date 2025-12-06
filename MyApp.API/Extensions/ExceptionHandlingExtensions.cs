using MyApp.API.Middleware;

namespace MyApp.API.Extensions
{
    public static class ExceptionHandlingExtensions
    {
        /// <summary>
        /// Global exception handling'i yapılandırır.
        /// IExceptionHandler kullanarak merkezi exception handling sağlar.
        /// </summary>
        public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }

        /// <summary>
        /// Exception handler middleware'ini pipeline'a ekler.
        /// </summary>
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler();

            return app;
        }
    }
}

