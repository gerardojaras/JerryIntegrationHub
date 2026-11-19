using System.Net;
using JerryIntegrationHub.Dtos;
using JerryIntegrationHub.Exceptions;

namespace JerryIntegrationHub.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
   
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            
            var correlationId = Guid.NewGuid().ToString();
            
            logger.LogError(exception, "Unhandled exception. CorrelationId: {correlationId}", correlationId);

            if (exception is BusinessException businessException)
            {
                context.Response.StatusCode = businessException.StatusCode;
                
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Code = businessException.Code,
                    Message = businessException.Message,
                    CorrelationId = correlationId
                });
                return;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorResponse
            {
                Code = "UNEXPECTED_ERROR",
                Message = "An unexpected error occurred. Please try again later.",
                CorrelationId = correlationId
            };
            
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}