using System;
using System.Collections.Generic;
using System.Linq;
using Dubstep.TestUtilities.TestLogger;
using Microsoft.Extensions.Logging;

namespace Dubstep.TestUtilities
{
    public class TestLogger<T> : ILogger<T>
    {
        public List<LogStatement> LogStatements { get; set; }

        public TestLogger()
        {
            LogStatements = new List<LogStatement>() { };
        }

        public LogStatement LastStatement => LogStatements.LastOrDefault();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var statement = new LogStatement()
            {
                Level = logLevel,
                Exception = exception,
                Message = formatter(state, exception)
            };

            LogStatements.Add(statement);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
