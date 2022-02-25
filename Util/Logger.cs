using System;
using System.Diagnostics;
using System.IO;

namespace Util
{
    public class Logger
    {
        private static EventLog _evtLog;

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
            WriteLogFile($"ERROR: {message}");
        }

        public static void Exception(Exception ex) { Error(ex.Message); Error(ex.StackTrace); }

        public static void Info(string message)
        {
            WriteLogFile($"INFO: {message}");
        }

        public static void Warning(string message)
        {
            WriteLogFile($"WARNING: {message}");
        }

        private static void WriteLogFile(string message, int tries = 0)
        {
            if (!File.Exists(Setting.LogFilePath))
            {
                InitFileLog();
                WriteLogFile(message, tries++);
                if (tries > 10) throw new Exception($"Unable to initialize the logfile at '{Setting.LogFilePath}'");
                return;
            }
            using (StreamWriter wfs = File.AppendText(Setting.LogFilePath))
            {
                wfs.WriteLineAsync($"[{DateTime.Now}] {message}");
            }
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
    }

    public enum LogType
    {
        ERROR,
        INFO,
        WARN,
    }
}
