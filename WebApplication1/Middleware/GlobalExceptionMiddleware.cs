using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace TaskApi.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
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
            catch (KeyNotFoundException ex)
            {
                await HandleNotFound(context, ex);
            }
            catch (InvalidOperationException ex)
            {
                await HandleConflict(context, ex);
            }
            catch (ArgumentException ex)
            {
                await HandleUnprocessableEntity(context, ex);
            }
            catch (Exception ex)
            {
                await HandleInternalServerError(context, ex);
            }
        }

        private async Task HandleNotFound(
            HttpContext context,
            Exception ex)
        {
            await WriteProblemDetails(
                context,
                404,
                "Resource Not Found",
                ex.Message);
        }

        private async Task HandleConflict(
            HttpContext context,
            Exception ex)
        {
            await WriteProblemDetails(
                context,
                409,
                "Conflict",
                ex.Message);
        }

        private async Task HandleUnprocessableEntity(
            HttpContext context,
            Exception ex)
        {
            await WriteProblemDetails(
                context,
                422,
                "Unprocessable Entity",
                ex.Message);
        }

        private async Task HandleInternalServerError(
            HttpContext context,
            Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");

            await WriteProblemDetails(
                context,
                500,
                "Internal Server Error",
                "An unexpected error occurred.");
        }

        private async Task WriteProblemDetails(
            HttpContext context,
            int statusCode,
            string title,
            string detail)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail
            };

            var json = JsonSerializer.Serialize(problemDetails);

            await context.Response.WriteAsync(json);
        }
    }
}