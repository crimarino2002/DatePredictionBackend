using Sales_Date_Prediction.SharedKernel.Dtos;
using System.Net;
using System.Text.Json;

namespace Sales_Date_Prediction.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private const string ContentType = "application/json";
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = ContentType;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new ApiResponse<string>
            {
                Status = ((int)HttpStatusCode.InternalServerError).ToString(),
                Message = exception.Message,
            };

            var result = JsonSerializer.Serialize(errorResponse);
            return context.Response.WriteAsync(result);
        }
    }
}
