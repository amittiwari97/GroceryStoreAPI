using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Helper
{
    public class GlobalExceptionHandler
    {
        //Declare Private property for Deleteage and Logging 
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalExceptionHandler(RequestDelegate next,
                                      ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// To create the delegate to invoke next middleware
        /// </summary>
        /// <param name="HttpContext"></param>
        /// <returns>Task</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        /// <summary>
        /// To Handle and Log the Error
        /// </summary>
        /// <param name="HttpContext"></param>
        /// <param name="Exception"></param>
        /// <param name="ILogger"></param>
        /// <returns>Task</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger _logger)
        {
            string message = exception.Message;
            string stackTrace = exception.StackTrace.ToString();
            string strException = exception.InnerException != null ? exception.InnerException.ToString() : string.Empty;

            // Serialize the data sturcture to send the error to cloud or filestorage
            var result = JsonSerializer.Serialize(new { error = !string.IsNullOrEmpty(strException) ? strException : message, stackTrace });
                         
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            _logger.LogError(result.ToString() + "\n" + result.ToString() + "\n\n");

            return context.Response.WriteAsync(result.ToString());

        }
    }    
}
