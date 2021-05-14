using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Dubstep.TestUtilities
{
    public class TestLogger<T> : ILogger<T>
    {
        public List<string> Messages { get; }
        public List<Exception> Exceptions { get; }
        public TestLogger()
        {
            Messages = new List<string>();
            Exceptions = new List<Exception>();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (exception != null)
            {
                Exceptions.Add(exception);
            }

            var result = formatter(state, exception);
            Messages.Add(result);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
