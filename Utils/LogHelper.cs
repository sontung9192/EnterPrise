using System;
using System.Diagnostics;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Utils
{
    public static class LogHelper
    {

        private static volatile Logger _log;

        //private static Logger logger = LogManager.GetCurrentClassLogger();

        private static Logger Log
        {
            get
            {
                if (_log == null)
                {
                    try
                    {
                        if (LogManager.Configuration != null && LogManager.Configuration.LoggingRules.Count > 0)
                        {
                            _log = LogManager.GetCurrentClassLogger();

                            return _log;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    FileTarget target = new FileTarget();
                    target.Layout = "${longdate} ${logger} ${message}";
                    target.FileName = "${basedir}/_LOG/${date:format=yyyyMMdd}.txt";
                    target.ArchiveFileName = "${basedir}/archives/${date:format=yyyyMMdd}_bak.txt";
                    target.ArchiveAboveSize = 1024*1024*2046; // archive files greater than 2 GB
                    target.Name = "Debug";
                    target.ConcurrentWrites = true;
                    SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);
                    FileTarget warning = new FileTarget();
                    warning.Layout = "${longdate} ${logger} ${message}";
                    warning.FileName = "${basedir}/_LOG/${date:format=yyyyMMdd}.txt";
                    warning.ArchiveFileName = "${basedir}/archives/${date:format=yyyyMMdd}_bak.txt";
                    warning.ArchiveAboveSize = 1024*1024*2047; // archive files greater than 10 KB
                    warning.Name = "Warning";
                    warning.ConcurrentWrites = true;
                    SimpleConfigurator.ConfigureForTargetLogging(warning, LogLevel.Warn);
                    _log = LogManager.GetLogger("Event");
                }

                return _log;

            }
        }

        public static void PublishException(Exception ex)
        {
            Log.Error(":\t" + GetCalleeString() + Environment.NewLine + "\t" + ex.Message + Environment.NewLine +
                      ex.StackTrace);
        }

        public static void LogMessage(string message)
        {

            Log.Error(":\t" + message);
        }

        public static void LogMessage(string message, string dir, string idLog = "")
        {
            if (string.IsNullOrEmpty(idLog))
                idLog = Config.MakeRefId();
            message = idLog + " - " +message;
            if (!string.IsNullOrEmpty(dir))
            {
                FileTarget target = new FileTarget();
                //target.Layout = "${longdate} ${logger} ${message}";
                target.Layout = "${longdate} ${message}";
                target.FileName = "${basedir}/_LOG/" + dir + "/${date:format=yyyyMMdd}_Log.txt";
                target.ArchiveFileName = "${basedir}/archives/${date:format=yyyyMMdd}_Log_bak.txt";
                target.ArchiveAboveSize = 1024*1024*2046; // archive files greater than 2G
                target.Name = "Debug";
                target.ConcurrentWrites = true;
                target.ConcurrentWrites = true;
                SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);
                Log.Factory.Configuration.AddTarget(dir, target);
                Log.Error(":\t" + message);
            }
            else
                LogMessage(message);
        }

        public static string LogInfo(string message, string dir = "Common", string idLog = "")
        {
            if (string.IsNullOrEmpty(idLog))
                idLog = Config.MakeRefId();
            if (string.IsNullOrEmpty(dir))
                dir = "Common";
            message = idLog + " - " + message;
            if (!string.IsNullOrEmpty(dir))
            {
                FileTarget target = new FileTarget();
                target.Layout = "${longdate} ${message}";       // ${logger} 
                target.FileName = @"C:/_LOG/" + dir + "/${date:format=yyyyMMdd}_log.txt";
                target.ArchiveFileName = "${basedir}/archives/${date:format=yyyyMMdd}_log_bak.txt";
                target.ArchiveAboveSize = 1024*1024*2047; // archive files greater than 10 KB
                target.Name = "Debug";
                target.ConcurrentWrites = true;
                target.ConcurrentWrites = true;
                SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);
                Log.Factory.Configuration.AddTarget(dir, target);
                //Log.Error(":\t" + GetCalleeString() + Environment.NewLine + "\t" + message);
                Log.Error(":\t" + message);
            }
            else
                LogMessage(message);
            return idLog;
        }
        public static string LogInfoFolder(string message, string dir = "Common", string idLog = "")
        {
            if (string.IsNullOrEmpty(idLog))
                idLog = Config.MakeRefId();
            if (string.IsNullOrEmpty(dir))
                dir = "Common";
            message = idLog + " - " + message;
            if (!string.IsNullOrEmpty(dir))
            {
                FileTarget target = new FileTarget();
                target.Layout = "${longdate} ${message}";       // ${logger} 
                target.FileName = @"C:/_LOG/" + dir + "/${date:format=yyyyMMdd}_log.txt";
                target.ArchiveFileName = "${basedir}/archives/${date:format=yyyyMMdd}_log_bak.txt";
                target.ArchiveAboveSize = 1024 * 1024 * 2047; // archive files greater than 10 KB
                target.Name = "Debug";
                target.ConcurrentWrites = true;
                target.ConcurrentWrites = true;
                SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);
                Log.Factory.Configuration.AddTarget(dir, target);
                //Log.Error(":\t" + GetCalleeString() + Environment.NewLine + "\t" + message);
                Log.Error(":\t" + message);
            }
            else
                LogMessage(message);
            return idLog;
        }
        public static void Warning(string message)
        {
            Log.Warn(":\t" + GetCalleeString() + Environment.NewLine + "\t" + message);
        }

        private static string GetCalleeString()
        {
            foreach (StackFrame sf in new StackTrace().GetFrames())
            {
                return string.Format("{0}.{1}", sf.GetMethod().ReflectedType.Name, sf.GetMethod().Name);
            }

            return string.Empty;
        }
    }
}