using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyApp.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace MyApp.API.Middleware
{
    /// <summary>
    /// Global exception handler - ASP.NET Core 8 IExceptionHandler implementasyonu
    /// Problem Details (RFC 7807) standardına uygun response döner
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger,
            IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception,
                "Exception occurred: {Message}",
                exception.Message);

            var problemDetails = CreateProblemDetails(httpContext, exception);
            var json = JsonSerializer.Serialize(problemDetails);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;

            await httpContext.Response.WriteAsync(json, cancellationToken);

            return true;
        }

        private ProblemDetails CreateProblemDetails(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = GetTitle(exception),
                Detail = GetDetail(exception),
                Type = GetType(exception),
                Instance = httpContext.Request.Path
            };

            // TraceId ekle (Activity.Current?.Id veya httpContext.TraceIdentifier)
            if (httpContext.TraceIdentifier != null)
            {
                problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
            }

            // Development modunda stack trace ekle
            if (_environment.IsDevelopment())
            {
                problemDetails.Extensions["stackTrace"] = exception.StackTrace;
                problemDetails.Extensions["innerException"] = exception.InnerException?.Message;
            }

            // Validation errors için ek bilgiler
            if (exception is BadRequestException badRequestException)
            {
                problemDetails.Extensions["errors"] = new { message = badRequestException.Message };
            }

            return problemDetails;
        }

        private int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                BadRequestException => (int)HttpStatusCode.BadRequest,
                UnauthorizedException => (int)HttpStatusCode.Unauthorized,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };
        }

        private string GetTitle(Exception exception)
        {
            return exception switch
            {
                NotFoundException => "Not Found",
                BadRequestException => "Bad Request",
                UnauthorizedException => "Unauthorized",
                UnauthorizedAccessException => "Unauthorized",
                ArgumentException => "Bad Request",
                InvalidOperationException => "Bad Request",
                _ => "An error occurred while processing your request"
            };
        }

        private string? GetDetail(Exception exception)
        {
            // Production'da genel mesaj, Development'da detaylı mesaj
            if (_environment.IsDevelopment())
            {
                return exception.Message;
            }

            // Production'da hassas bilgileri gizle
            return exception switch
            {
                NotFoundException => exception.Message,
                BadRequestException => exception.Message,
                UnauthorizedException => exception.Message,
                UnauthorizedAccessException => exception.Message,
                ArgumentException => exception.Message,
                InvalidOperationException => exception.Message,
                _ => "An error occurred while processing your request. Please try again later."
            };
        }

        private string GetType(Exception exception)
        {
            return exception switch
            {
                NotFoundException => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                BadRequestException => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                UnauthorizedException => "https://tools.ietf.org/html/rfc7235#section-3.1",
                UnauthorizedAccessException => "https://tools.ietf.org/html/rfc7235#section-3.1",
                ArgumentException => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                InvalidOperationException => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };
        }
    }
}



