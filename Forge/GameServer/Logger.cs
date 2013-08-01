using System.IO;
using log4net;

namespace BlastersShared
{
    /// <summary>
    /// A basic logging class that allows logging to specific 
    /// </summary>
    public class Logger
    {
        private ILog _log;

        public Logger()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("config.xml"));
            _log = log4net.LogManager.GetLogger(typeof(Logger));
        }

        private static readonly Logger _instance = new Logger();

        /// <summary>
        /// Return an entry point for this logger
        /// </summary>
        public static Logger Instance { get { return _instance; } }

        public void Log(Level level, string message)
        {
            switch (level)
            {
                case Level.Debug:
                    _log.Debug(message);
                    break;

                case Level.Info:
                    _log.Info(message);
                    break;

                case Level.Warn:
                    _log.Warn(message);
                    break;

                case Level.Error:
                    _log.Error(message);
                    break;

                case Level.Fatal:
                    _log.Fatal(message);
                    break;
            }
        }

    }

    public enum Level
    {
        Debug = 5,
        Info = 4,
        Warn = 3,
        Error = 2,
        Fatal = 1,
    }
}



