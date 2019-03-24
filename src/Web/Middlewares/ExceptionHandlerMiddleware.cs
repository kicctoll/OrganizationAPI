using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Web.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandlingExceptionAsync(context, exception);
            }
        }

        private async Task HandlingExceptionAsync(HttpContext context, Exception exception)
        {
            var request = context.Request;
            var fullPath = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString.Value}";

            var bodyResponse = new
            {
                request.Method,
                Path = fullPath,
                exception.Message
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(bodyResponse));
        }
    }
}
