using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging
{
    public class FileJsonLogger : ILogger
    {
        private readonly string _infoFilePath;
        private readonly string _errorsFilePath;
        private readonly object _locker = new object();

        public FileJsonLogger(string folder, string infoFileName, string errorsFileName)
        {
            _infoFilePath = Path.Combine(folder, infoFileName);
            _errorsFilePath = Path.Combine(folder, errorsFileName);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Information || logLevel == LogLevel.Error;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_locker)
                {
                    string path = "";

                    if (logLevel == LogLevel.Information)
                    {
                        path = _infoFilePath;
                    }
                    else if (logLevel == LogLevel.Error)
                    {
                        path = _errorsFilePath;
                    }

                    if (!File.Exists(path))
                    {
                        using (File.Create(path)) { }
                    }

                    string content = File.ReadAllText(path);

                    if (!string.IsNullOrEmpty(content))
                    {
                        var regex = new Regex(@"]$", RegexOptions.IgnoreCase);

                        content = regex.Replace(content, $",{formatter(state, exception)}]");
                    }
                    else
                    {
                        content = $"[{formatter(state, exception)}]";
                    }

                    File.WriteAllText(path, content);
                }
            }
        }
    }
}
