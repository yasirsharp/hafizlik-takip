using System.Net;
using FluentValidation;

namespace WebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";

            if (e is ValidationException validationException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = string.Join(", ", validationException.Errors.Select(x => x.ErrorMessage));
            }
            else if (e.Message.Contains("Yetkiniz yok"))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                message = e.Message;
            }
            else
            {
                message = e.Message;
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
