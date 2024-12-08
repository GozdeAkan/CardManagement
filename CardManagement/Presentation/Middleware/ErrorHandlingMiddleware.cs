using System.Net;
using Newtonsoft.Json;

namespace Presentation.Middleware
{
    /// <summary>
    /// Middleware for handling global errors and returning consistent error responses.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor to initialize the middleware.
        /// </summary>
        /// <param name="next">Next middleware in the pipeline.</param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// Middleware entry point that wraps the request pipeline with error handling.
        /// </summary>
        /// <param name="context">HTTP context for the current request.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle exceptions and generate a response.
                await HandleExceptionAsync(context, ex);
            }
        }
        /// <summary>
        /// Handles exceptions and generates a JSON response with appropriate HTTP status codes.
        /// </summary>
        /// <param name="context">HTTP context for the current request.</param>
        /// <param name="exception">Exception to handle.</param>
        /// <returns>A task representing the asynchronous operation of writing the response.</returns>

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError; // Default status code
            var message = exception.Message ?? "An unexpected error occurred."; // Default error message

            // Customize the response for specific exception types.
            if (exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = "Unauthorized access.";
            }
            else if (exception is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
            }
            else if (exception is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                message = "The requested resource was not found.";
            }
            // Log the error to the console.
            Console.WriteLine($"Error: {exception.Message}");

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }

}
