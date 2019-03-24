using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Logging
{
    public class LoggingObject
    {
        public string Date
        {
            get
            {
                return $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";
            }
        }

        public LoggingRequest Request { get; private set; }

        public LoggingResponse Response { get; private set; }

        public LoggingObject(LoggingRequest request, LoggingResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}
