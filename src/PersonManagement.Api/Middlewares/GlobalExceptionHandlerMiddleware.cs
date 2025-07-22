using PersonManagement.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace PersonManagement.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Default to 500
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred.";

            switch (exception)
            {
                case PersonNotFoundException pnfEx:
                    statusCode = HttpStatusCode.NotFound;
                    message = pnfEx.Message;
                    break;
                case PersonAlreadyExistsException paeEx:
                    statusCode = HttpStatusCode.Conflict;
                    message = paeEx.Message;
                    break;
                case RelationAlreadyExistsException raeEx:
                    statusCode = HttpStatusCode.Conflict;
                    message = raeEx.Message;
                    break;
                case RelationDoesNotExistException rdneEx:
                    statusCode = HttpStatusCode.NotFound;
                    message = rdneEx.Message;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var jsonMessage = JsonSerializer.Serialize(message);
            return context.Response.WriteAsync(jsonMessage);
        }
    }
}
