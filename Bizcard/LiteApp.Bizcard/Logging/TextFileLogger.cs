using System;
using System.IO;

namespace LiteApp.Bizcard.Logging
{
    public class TextFileLogger : ILogger
    {
        public event EventHandler<LoggingFailedEventArgs> LoggingFailed;

        public void Information(string detail, params string[] parameters)
        {
            Log(string.Format(detail, parameters), LogLevel.Information);
        }

        public void Warning(string detail, params string[] parameters)
        {
            Log(string.Format(detail, parameters), LogLevel.Warning);
        }

        public void Error(string detail, params string[] parameters)
        {
            Log(string.Format(detail, parameters), LogLevel.Error);
        }

        void Log(string logMessage, LogLevel level)
        {
            StreamWriter w = null;
            string filePath = null;
            try
            {
                filePath = GetLogFilePath();
                using (w = File.AppendText(filePath))
                {
                    WriteLog(logMessage, level, w);
                    // Close the writer and underlying file.
                    w.Close();
                }
            }
            catch (DirectoryNotFoundException)
            {
                // If directory not found (deleted by user due to a mistake), so we need to create one and try again
                string directory = Path.GetDirectoryName(filePath);
                try
                {
                    Directory.CreateDirectory(directory);
                    Log(logMessage, level); // Write again
                }
                catch
                {
                    HandleLoggingFailed(logMessage, level);
                }
            }
            catch
            {
                HandleLoggingFailed(logMessage, level);
            }
        }

        void HandleLoggingFailed(string logMessage, LogLevel level)
        {
            if (LoggingFailed != null)
                LoggingFailed(this, new LoggingFailedEventArgs() { LogMessage = logMessage, LogLevel = level });
        }

        private static string GetLogFilePath()
        {
            string logFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Bizcard", "Log");

            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            var fileName = string.Format("{0}.txt", DateTime.Now.ToString("yyyy-MM-dd_HH"));

            return Path.Combine(logFolder, fileName);
        }

        static void WriteLog(string logMessage, LogLevel level, TextWriter w)
        {
            w.WriteLine("{0} on {1} {2}", level, DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  ");
            w.WriteLine("  {0}", logMessage);
            w.WriteLine("-------------------------------");
            w.WriteLine("");
            // Update the underlying file.
            w.Flush();
        }

        static void DumpLog(StreamReader r)
        {
            // While not at the end of the file, read and write lines.
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            r.Close();
        }
    }
}
