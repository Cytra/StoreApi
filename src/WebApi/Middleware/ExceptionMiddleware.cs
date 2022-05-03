using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace StoreApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error - {ex.Message}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(JsonSerializer.Serialize(GetErrorResponse(exception)));
    }

    private ErrorResponse GetErrorResponse(Exception exception)
    {
        var result = new ErrorResponse();
        result.Errors.Add(new Error { ErrorReason = "Internal Server Error" });
        return result;
    }
}