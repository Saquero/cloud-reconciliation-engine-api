using CleanDddHexagonal.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace CleanDddHexagonal.Api.Middleware;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException exception)
        {
            await WriteErrorAsync(context, HttpStatusCode.BadRequest, exception.Message);
        }
        catch (ArgumentException exception)
        {
            await WriteErrorAsync(context, HttpStatusCode.BadRequest, exception.Message);
        }
        catch (Exception)
        {
            await WriteErrorAsync(context, HttpStatusCode.InternalServerError, "Unexpected server error.");
        }
    }

    private static async Task WriteErrorAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var payload = JsonSerializer.Serialize(new
        {
            status = context.Response.StatusCode,
            error = message
        });

        await context.Response.WriteAsync(payload);
    }
}
