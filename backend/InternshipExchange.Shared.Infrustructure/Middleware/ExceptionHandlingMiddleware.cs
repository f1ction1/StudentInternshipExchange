using Modules.Common.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Modules.Common.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ConflictException ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status409Conflict, ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (BadRequestException ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (FluentValidation.ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                context.Response.ContentType = "application/json";
                var errorObj = new { errors = ex.Errors.Select(e => e.ErrorMessage) };
                var json = JsonSerializer.Serialize(errorObj);
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError, ex.Message ?? "An unexpected error occurred.");
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var errorObj = new { error = message };
            var json = JsonSerializer.Serialize(errorObj);
            await context.Response.WriteAsync(json);
        }
    }
}
