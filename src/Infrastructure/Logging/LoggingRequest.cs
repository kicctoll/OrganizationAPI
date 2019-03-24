using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Logging
{
    public class LoggingRequest
    {
        public string Method { get; set; }
        
        public string URI { get; set; }

        public dynamic Body { get; set; }

        public LoggingRequest(string method, string uri, dynamic body)
        {
            this.Method = method;
            this.URI = uri;
            this.Body = body;
        }
    }
}
