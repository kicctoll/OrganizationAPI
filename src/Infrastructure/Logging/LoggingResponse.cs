using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Logging
{
    public class LoggingResponse
    {
        public short StatusCode { get; set; }
        
        public dynamic Body { get; set; }

        public LoggingResponse(short statusCode, dynamic body)
        {
            StatusCode = statusCode;
            Body = body;
        }
    }
}
