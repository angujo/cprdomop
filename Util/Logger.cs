using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Util
{
    public class Logger
    {
        private static EventLog _evtLog;
        private static ConcurrentBag<LogHolder> logs = new ConcurrentBag<LogHolder>();
        private static bool bagging { get { return logs.Count > 0; } }

        public static void EvtLog(string message, LogType type = LogType.ERROR)
        {
            EventLogEntryType et = 0;
            switch (type)
            {
                case LogType.INFO: et = EventLogEntryType.Information; break;
                case LogType.WARN: et = EventLogEntryType.Warning; break;
                default: et = EventLogEntryType.Error; break;
            }
            InitEventLog();
            _evtLog.WriteEntry(message, et);
        }

        public static void Error(string message)
        {
            WriteLogFile(message, LogType.ERROR, DateTime.Now);
        }

        public static void Exception(Exception ex) { Error(ex.Message); Error(ex.StackTrace); }

        public static void Info(string message)
        {
            WriteLogFile(message, LogType.INFO, DateTime.Now);
        }

        public static void Warning(string message)
        {
            WriteLogFile(message, LogType.WARN, DateTime.Now);
        }

        private static void WriteLogFile(string message, LogType type, DateTime dated, int tries = 0)
        {
            if (!File.Exists(Setting.LogFilePath))
            {
                InitFileLog();
                WriteLogFile(message, type, dated, tries++);
                if (tries > 10) throw new Exception($"Unable to initialize the logfile at '{Setting.LogFilePath}'");
                return;
            }
            Task.Run(() =>
            {
                try
                {
                    using (StreamWriter wfs = File.AppendText(Setting.LogFilePath))
                    {
                        while (logs.TryTake(out var bg))
                        {
                            wfs.WriteLineAsync($"[{bg.Dated:yyyy-MM-dd H:mm:ss.FFFFFF}#EXC] {bg.Type.GetStringValue()}: {bg.Message}");
                        }
                        wfs.WriteLineAsync($"[{dated:yyyy-MM-dd H:mm:ss.FFFFFF}] {type.GetStringValue()}: {message}");
                    }
                }
                catch (Exception)
                {
                    logs.Add(new LogHolder { Dated = DateTime.Now, Message = message, Type = type });
                    // initRunner();
                    // throw;
                }
            });

        }

        private static void WriteLogFile(LogHolder logHolder)
        {
            WriteLogFile(logHolder.Message, logHolder.Type, logHolder.Dated);
        }

        public static void InitEventLog()
        {
            if (!EventLog.SourceExists(Setting.SERVICE_SOURCE_NAME))
            {
                EventLog.CreateEventSource(Setting.SERVICE_SOURCE_NAME, Setting.SERVICE_LOG_NAME);
            }

            if (null == _evtLog)
            {
                _evtLog = new EventLog();
                _evtLog.Source = Setting.SERVICE_SOURCE_NAME;
                _evtLog.Log = Setting.SERVICE_LOG_NAME;
            }
        }

        public static void InitFileLog()
        {
            if (!Directory.Exists(Setting.LogDirectoryPath)) Directory.CreateDirectory(Setting.LogDirectoryPath);
            if (File.Exists(Setting.LogFilePath)) return;
            using (FileStream wfs = File.Create(Setting.LogFilePath))
            using (BufferedStream wbs = new BufferedStream(wfs))
            using (StreamWriter sw = new StreamWriter(wbs))
            {
                sw.WriteLineAsync($"------NEW LOG FILE [{DateTime.Now}]------");
            }
        }

        private static void initRunner()
        {
            if (bagging) return;
            Task.Run(async () =>
            {
                while (bagging)
                {
                    await Task.Delay(200);
                    if (logs.TryTake(out LogHolder logHolder))
                    {
                        WriteLogFile(logHolder);
                    }
                }
            });
        }
    }

    public enum LogType
    {
        [StringValue("ERROR")]
        ERROR,
        [StringValue("INFO")]
        INFO,
        [StringValue("WARNING")]
        WARN,
    }

    internal class LogHolder
    {
        public LogType Type { get; set; }
        public string Message { get; set; }
        public DateTime Dated { get; set; }
    }
}
