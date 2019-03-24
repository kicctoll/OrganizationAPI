using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Web.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly FileJsonLogger _fileLogger;
        
        public LoggingMiddleware(RequestDelegate next, FileJsonLogger fileLogger)
        {
            _next = next;
            _fileLogger = fileLogger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyResponse = context.Response.Body;

            try
            {
                using (var responseBodyMemory = new MemoryStream())
                {
                    context.Response.Body = responseBodyMemory;
                    context.Request.EnableRewind();

                    var requestLog = await CreateRequestLogObject(context.Request);
                    context.Request.Body.Position = 0;

                    await _next(context);

                    responseBodyMemory.Position = 0;
                    var responseLog = await CreateResponseLogObject(context.Response);

                    var objectLog = new LoggingObject(requestLog, responseLog);

                    if (responseLog.StatusCode == StatusCodes.Status500InternalServerError)
                    {
                        _fileLogger.LogError(JsonConvert.SerializeObject(objectLog));
                    }
                    else
                    {
                        _fileLogger.LogInformation(JsonConvert.SerializeObject(objectLog));
                    }

                    responseBodyMemory.Position = 0;
                    await responseBodyMemory.CopyToAsync(originalBodyResponse);
                }
            }
            finally
            {
                context.Response.Body = originalBodyResponse;
            }
        }

        private async Task<LoggingRequest> CreateRequestLogObject(HttpRequest request)
        {
            var method = request.Method;
            var uri = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString.Value}";
            dynamic body = JsonConvert.DeserializeObject(await new StreamReader(request.Body).ReadToEndAsync());

            return new LoggingRequest(method, uri, body);
        }

        private async Task<LoggingResponse> CreateResponseLogObject(HttpResponse response)
        {
            var statusCode = (short)response.StatusCode;
            dynamic body = JsonConvert.DeserializeObject(await new StreamReader(response.Body).ReadToEndAsync());

            return new LoggingResponse(statusCode, body);
        }
    }
}
