using Microsoft.AspNetCore.Mvc;

namespace Cinema.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
            // 404 
            catch (MovieNotFoundException ex)
            {
                await WriteProblemDetails(context, StatusCodes.Status404NotFound, "Movie not found", ex.Message);
            }
            catch (AuditoriumNotFoundException ex)
            {
                await WriteProblemDetails(context, StatusCodes.Status404NotFound, "Auditorium not found", ex.Message);
            }
            catch (ShowTimeNotFoundException ex)
            {
                await WriteProblemDetails(context, StatusCodes.Status404NotFound, "ShowTime not found", ex.Message);
            }
            catch (CustomerNotFoundException ex)
            {
                await WriteProblemDetails(context, StatusCodes.Status404NotFound, "Customer not found", ex.Message);
            }
            catch (BookingNotFoundException ex)
            {
                await WriteProblemDetails(context, StatusCodes.Status404NotFound, "Booking not found", ex.Message);
            }
            // 409 
            catch (MovieAlreadyExistsException ex)
            {
                await WriteProblemDetails(context, StatusCodes.Status409Conflict, "Movie already exists", ex.Message);
            }
            catch (DeleteConflictException ex)
            {
                await WriteProblemDetails(context, StatusCodes.Status409Conflict, "Delete conflict", ex.Message);
            }
            // 422 
            catch (InvalidBookingException ex)
            {
                await WriteProblemDetails(context, StatusCodes.Status422UnprocessableEntity, "Invalid booking", ex.Message);
            }
            // 500
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await WriteProblemDetails(
                    context,
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred.",
                    "Please contact support with your traceId.");
            }
        }

        private static async Task WriteProblemDetails(HttpContext context, int status, string title, string detail)
        {
            context.Response.StatusCode = status;
            context.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = detail,
                Instance = context.Request.Path
            };

            problem.Extensions["traceId"] = context.TraceIdentifier;

            await context.Response.WriteAsJsonAsync(problem);
        }
    }

    [Serializable]
    internal class InvalidBookingException : Exception
    {
        public InvalidBookingException()
        {
        }

        public InvalidBookingException(string? message) : base(message)
        {
        }

        public InvalidBookingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class DeleteConflictException : Exception
    {
        public DeleteConflictException()
        {
        }

        public DeleteConflictException(string? message) : base(message)
        {
        }

        public DeleteConflictException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class MovieAlreadyExistsException : Exception
    {
        public MovieAlreadyExistsException()
        {
        }

        public MovieAlreadyExistsException(string? message) : base(message)
        {
        }

        public MovieAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class BookingNotFoundException : Exception
    {
        public BookingNotFoundException()
        {
        }

        public BookingNotFoundException(string? message) : base(message)
        {
        }

        public BookingNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException()
        {
        }

        public CustomerNotFoundException(string? message) : base(message)
        {
        }

        public CustomerNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class ShowTimeNotFoundException : Exception
    {
        public ShowTimeNotFoundException()
        {
        }

        public ShowTimeNotFoundException(string? message) : base(message)
        {
        }

        public ShowTimeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class AuditoriumNotFoundException : Exception
    {
        public AuditoriumNotFoundException()
        {
        }

        public AuditoriumNotFoundException(string? message) : base(message)
        {
        }

        public AuditoriumNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class MovieNotFoundException : Exception
    {
        public MovieNotFoundException()
        {
        }

        public MovieNotFoundException(string? message) : base(message)
        {
        }

        public MovieNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
