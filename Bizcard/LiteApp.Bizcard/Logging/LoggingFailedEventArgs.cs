using System;

namespace LiteApp.Bizcard.Logging
{
    public class LoggingFailedEventArgs : EventArgs
    {
        public string LogMessage { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
