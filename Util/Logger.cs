using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class Logger
    {
        readonly static string Name = "OMOPApp";
        readonly static string SourceName = "WinOMOPService";
        private static EventLog _evtLog;
        private static string file_path;

        public static void EvtLog(string message, LogType type = LogType.ERROR)
        {
            EventLogEntryType et = 0;
            switch (type)
            {
                case LogType.INFO: et = EventLogEntryType.Information; break;
                case LogType.WARN: et = EventLogEntryType.Warning; break;
                default: et = EventLogEntryType.Error; break;
            }
            if (!EventLog.SourceExists(SourceName)) return;
            if (null == _evtLog)
            {
                _evtLog = new EventLog();
                _evtLog.Source = SourceName;
            }
            _evtLog.WriteEntry(message, et);
        }

        public static void Error(string message)
        {
            WriteLogFile($"[{DateTime.Now}] ERROR: {message}");
        }

        public static void Exception(Exception ex) { Error(ex.Message); Error(ex.StackTrace); }

        public static void Info(string message)
        {
            WriteLogFile($"[{DateTime.Now}] INFO: {message}");
        }

        public static void Warning(string message)
        {
            WriteLogFile($"[{DateTime.Now}] WARNING: {message}");
        }

        private static void WriteLogFile(string message, int tries = 0)
        {
            if (string.IsNullOrEmpty(file_path) || !File.Exists(file_path))
            {
                InitFileLog();
                WriteLogFile(message, tries++);
                if (tries > 10) return;
                return;
            }
            using (StreamWriter wfs = File.AppendText(file_path))
            {
                wfs.WriteLineAsync($"[{DateTime.Now}] {message}");
            }
        }

        public static void InitEventLog()
        {
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, Name);
            }
        }

        public static void InitFileLog()
        {
            string dir_path = Path.Combine(Environment.CurrentDirectory, "log");
            if (!Directory.Exists(file_path)) Directory.CreateDirectory(dir_path);
            file_path = Path.Combine(dir_path, "log.txt");
            if (File.Exists(file_path)) return;
            using (FileStream wfs = File.Create(file_path))
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
