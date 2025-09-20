using System.Net;
using System.Text.Json;
using LeCiel.DTOs.Responses;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Middlewares;

public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleware> logger
)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger = logger;
    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException exception)
        {
            _logger.LogError(exception, "Database update exception");
            SetHttpResponseFields(context, HttpStatusCode.BadRequest);

            var message =
                exception.InnerException?.Message.Contains("Duplicate entry") == true
                    ? ExtractClientPart(exception.InnerException?.Message)
                    : "Database update error.";
            await CreateAndWriteResponse(context, message);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception occurred");
            SetHttpResponseFields(context, HttpStatusCode.InternalServerError);
            var message = "Internal Server Error";
            await CreateAndWriteResponse(context, message);
        }
    }

    private static string ExtractClientPart(string? message)
    {
        if (message == null)
        {
            return "";
        }
        var stop = message.IndexOf("for key");
        var clientPart = message.Substring(0, stop).Trim();
        return clientPart;
    }

    private static void SetHttpResponseFields(HttpContext context, HttpStatusCode statusCode)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
    }

    private static async Task CreateAndWriteResponse(HttpContext context, string message)
    {
        var response = new GenericResponse<object>(
            false,
            new { context.Response.StatusCode, message }
        );
        var json = JsonSerializer.Serialize(response, jsonSerializerOptions);
        await context.Response.WriteAsync(json);
    }
}
