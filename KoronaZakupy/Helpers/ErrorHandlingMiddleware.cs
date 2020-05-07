using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace KoronaZakupy.Helpers
{
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
                await HandleExceptionAsync(context,ex);
            }

        }  
        private static Task HandleExceptionAsync(HttpContext context,Exception ex)
            {
            var httpCode = HttpStatusCode.InternalServerError;
            var message = "Internal Server Error";

            if (ex is ApplicationException)
                message = ex.Message;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpCode;

            return context.Response.WriteAsync(new ErrorDetails((int)httpCode, message).ToString());
        }
        
    }
}
