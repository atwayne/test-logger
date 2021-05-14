using System;
using Microsoft.Extensions.Logging;

namespace Dubstep.TestUtilities.TestLogger
{
    public class LogStatement
    {
        public LogLevel Level { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
