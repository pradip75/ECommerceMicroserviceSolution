using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GlobalErrorHandling
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;
        public GlobalErrorHandlerMiddleware(
            RequestDelegate next, 
            ILogger<GlobalErrorHandlerMiddleware> logger)
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
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred: {Message}", e.Message);
                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = "An internal server has occurred"
                };
                string json = System.Text.Json.JsonSerializer.Serialize(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}
