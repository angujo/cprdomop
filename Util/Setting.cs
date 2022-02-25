using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Principal;

namespace Util
{
    public static class Setting
    {
        public const string APP_NAME = "OMOPAPP";
        public const string SERVICE_SOURCE_NAME = "OMOPAPPService";
        public const string SERVICE_LOG_NAME = "OMOPAPPLog";
        public const string LOG_FILE_NAME = "log.txt";
        public const string DB_FILE_NAME = "database.sqlite3";

        public static string DBDirectoryPath
        {
            get
            {
                var v = GetRegistryValue("DBDirectory");
                return string.IsNullOrEmpty(v) ? Environment.CurrentDirectory : v;
            }
            set { SetRegistryValue("DBDirectory", value); }
        }
        public static string LogDirectoryPath
        {
            get
            {
                var v = GetRegistryValue("LogDirectory");
                return string.IsNullOrEmpty(v) ? Environment.CurrentDirectory : v;
            }
            set { SetRegistryValue("LogDirectory", value); }
        }
        public static string DBFilePath
        {
            get { return Path.Combine(DBDirectoryPath, DB_FILE_NAME); }
        }
        public static string LogFilePath
        {
            get { return Path.Combine(LogDirectoryPath, LOG_FILE_NAME); }
        }

        public static bool IsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent())
                .IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static string GetRegistryValue(string key)
        {
            RegistryKey appKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", false).OpenSubKey(APP_NAME, false);
            if (null == appKey) return null;
            return (string)appKey.GetValue(key);
        }

        private static void SetRegistryValue(string key, string value)
        {
            RegistryKey appKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true).CreateSubKey(APP_NAME, true);
            appKey.SetValue(key, value);
        }
    }
}
