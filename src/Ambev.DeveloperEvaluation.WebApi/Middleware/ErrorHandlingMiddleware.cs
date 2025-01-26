using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

/// <summary>
/// Middleware for handling exceptions globally and returning consistent error responses.
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        ErrorResponse errorResponse;

        switch (exception)
        {
            case ValidationException validationException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse = new ErrorResponse
                {
                    Type = "ValidationError",
                    Error = "Invalid input data",
                    Detail = validationException.Errors.FirstOrDefault()?.ErrorMessage ?? exception.Message
                };
                break;

            case DomainException domainException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse = new ErrorResponse
                {
                    Type = "DomainError",
                    Error = "Business rule violation",
                    Detail = domainException.Message
                };
                break;

            case InvalidOperationException invalidOperationException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse = new ErrorResponse
                {
                    Type = "InvalidOperation",
                    Error = "Invalid operation",
                    Detail = invalidOperationException.Message
                };
                break;

            case KeyNotFoundException keyNotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse = new ErrorResponse
                {
                    Type = "ResourceNotFound",
                    Error = "Resource not found",
                    Detail = keyNotFoundException.Message
                };
                break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse = new ErrorResponse
                {
                    Type = "ServerError",
                    Error = "An unexpected error occurred",
                    Detail = exception.Message
                };
                break;
        }

        var response = JsonSerializer.Serialize(errorResponse);
        return context.Response.WriteAsync(response);
    }
}
