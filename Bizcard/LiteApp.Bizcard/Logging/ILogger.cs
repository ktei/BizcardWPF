using System;

namespace LiteApp.Bizcard.Logging
{
    public interface ILogger
    {
        void Information(string detail, params string[] parameters);
        void Warning(string detail, params string[] parameters);
        void Error(string detail, params string[] parameters);
        event EventHandler<LoggingFailedEventArgs> LoggingFailed;
    }
}
