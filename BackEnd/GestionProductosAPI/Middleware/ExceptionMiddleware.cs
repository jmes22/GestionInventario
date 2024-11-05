using Entity.Common;
using Entity.Common.Exceptions;
using System.Text.Json;

namespace GestionProductosAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var errorDetails = new ErrorDetails
            {
                Message = "Internal Server Error"
            };

            switch (exception)
            {
                case ValidationException validationEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    errorDetails.Message = validationEx.Message;
                    errorDetails.ExceptionType = "Validation Error";
                    break;

                case NotFoundException notFoundEx:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    errorDetails.Message = notFoundEx.Message;
                    errorDetails.ExceptionType = "Not Found";
                    break;

                case UnauthorizedAccessException unauthorizedEx:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    errorDetails.Message = unauthorizedEx.Message;
                    errorDetails.ExceptionType = "Unauthorized";
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    errorDetails.ExceptionType = "Server Error";
                    // Solo incluir detalles técnicos en desarrollo
                    if (_env.IsDevelopment())
                    {
                        errorDetails.Message = exception.Message;
                        errorDetails.StackTrace = exception.StackTrace;
                    }
                    break;
            }

            errorDetails.StatusCode = context.Response.StatusCode;

            var result = JsonSerializer.Serialize(errorDetails, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(result);
        }
    }
}
