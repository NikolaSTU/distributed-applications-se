using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace DiabetesAPI.ErrorHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {

            _logger.LogError(exception, "Възникна необработена грешка: {Message}", exception.Message); 
            // log exception to console

            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Възникна вътрешна системна грешка. Моля, опитайте по-късно.";

            if (exception is KeyNotFoundException || exception is ArgumentException)
            {
                statusCode = HttpStatusCode.NotFound;
                message = exception.Message;
            }
            else if (exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = "Нямате достъп до този ресурс.";
            }

            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message,
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await httpContext.Response.WriteAsync(json, cancellationToken);

            return true;
        }
    }
}
