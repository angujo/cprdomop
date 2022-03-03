using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Security.Principal;

namespace Util
{
    public static class Setting
    {
        static string defPass = "angujomondi@gmail.com#2022#oxford#bortnar";
        public static string LOG_FILE_NAME { get { return "log.txt"; } }

        public const string APP_NAME = "OMOPAPP";
        public const string SERVICE_SOURCE_NAME = "OMOPAPPService";
        public const string SERVICE_LOG_NAME = "OMOPAPPLog";
        public const string DB_FILE_NAME = "database.sqlite3";

        public static string InstallationDirectory
        {
            get { return GetRegistryValue("InstallationDirectory"); }
            set { SetRegistryValue("InstallationDirectory", value); }
        }
        public static string DBDirectoryPath
        {
            get { return GetRegistryValue("DBDirectory") ?? Environment.CurrentDirectory; }
            set { SetRegistryValue("DBDirectory", value); }
        }
        public static string LogDirectoryPath
        {
            get { return GetRegistryValue("LogDirectory") ?? Environment.CurrentDirectory; }
            set { SetRegistryValue("LogDirectory", value); }
        }
        public static string ServiceName
        {
            get { return GetRegistryValue("ServiceName") ?? "OMOPService"; }
            set { SetRegistryValue("ServiceName", value); }
        }
        public static int DBConnection // Int value of DBMSType enums
        {
            get { return int.TryParse(GetRegistryValue("DBConnection"), out int dbc) ? dbc : 0; }
            set { SetRegistryValue("DBConnection", value.ToString()); }
        }
        public static string DBServer
        {
            get { return GetRegistryValue("DBServer"); }
            set { SetRegistryValue("DBServer", value); }
        }
        public static string DBPort
        {
            get { return GetRegistryValue("DBPort"); }
            set { SetRegistryValue("DBPort", value); }
        }
        public static string DBName
        {
            get { return GetRegistryValue("DBName"); }
            set { SetRegistryValue("DBName", value); }
        }
        public static string DBSchema
        {
            get { return GetRegistryValue("DBSchema"); }
            set { SetRegistryValue("DBSchema", value); }
        }
        public static string DBUsername
        {
            get { return GetRegistryValue("DBUsername"); }
            set { SetRegistryValue("DBUsername", value); }
        }
        public static string DBPassword
        {
            get { return GetRegistryValue("DBPassword"); }
            set { SetRegistryValue("DBPassword", value); }
        }
        public static string SysPassword { get { return GetRegistryValue("SysPassword") ?? defPass; } }
        public static string DBFilePath
        {
            get { return Path.Combine(DBDirectoryPath, DB_FILE_NAME); }
        }
        public static string LogFilePath
        {
            get { return Path.Combine(LogDirectoryPath, LOG_FILE_NAME); }
        }

        public static void SetSysPassword()
        {
            if (!SysPassword.Equals(defPass) || !IsAdmin()) return;
            SetRegistryValue("SysPassword", Guid.NewGuid().ToString());
        }

        public static bool IsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent())
                .IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static string GetRegistryValue(string key, bool userBased = false)
        {
            // 
            RegistryKey locKey = userBased ? Registry.CurrentUser : Registry.LocalMachine;
            RegistryKey appKey = locKey.OpenSubKey("SOFTWARE", false).OpenSubKey(APP_NAME, false);
            if (null == appKey) return null;
            var v = (string)appKey.GetValue(key);
            return String.IsNullOrEmpty(v) ? null : v;
        }

        private static void SetRegistryValue(string key, string value, bool userBased = false)
        {
            RegistryKey locKey = userBased ? Registry.CurrentUser : Registry.LocalMachine;
            RegistryKey appKey = locKey.OpenSubKey("SOFTWARE", true).CreateSubKey(APP_NAME, true);
            if (null == value)
            {
                if (appKey.GetValueNames().Contains(key)) appKey.DeleteValue(key);
            }
            else appKey.SetValue(key, value);
        }
    }
}
