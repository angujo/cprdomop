using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class Setting
    {
        public const string LOG_FILE_NAME = "log.txt";
        public const string DB_FILE_NAME = "database.sqlite3";

        public static string DBDirectoryPath
        {
            get { return Properties.Settings.Default.db_path; }
            set
            {
                Properties.Settings.Default.db_path = value;
                Properties.Settings.Default.Save();
            }
        }
        public static string LogDirectoryPath
        {
            get { return Properties.Settings.Default.log_path; }
            set
            {
                Properties.Settings.Default.log_path = value;
                Properties.Settings.Default.Save();
            }
        }
        public static string DBFilePath
        {
            get { return Path.Combine(DBDirectoryPath,DB_FILE_NAME); }
        }
        public static string LogFilePath
        {
            get { return Path.Combine(LogDirectoryPath,LOG_FILE_NAME); }
        }
    }
}
