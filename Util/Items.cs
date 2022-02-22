using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class Items
    {
        public const string LOG_FILE_NAME = "log.txt";
        public const string DB_FILE_NAME = "database.sqlite3";

        public static string LogFilePath(string directory_path = null)
        {
            if (string.IsNullOrEmpty(directory_path) || !Directory.Exists(directory_path)) directory_path = Path.Combine(Environment.CurrentDirectory, "log");
            return Path.Combine(directory_path, LOG_FILE_NAME);
        }

        public static string DBFilePath(string directory_path = null)
        {
            if (string.IsNullOrEmpty(directory_path) || !Directory.Exists(directory_path)) directory_path = Environment.CurrentDirectory;
            return Path.Combine(directory_path, DB_FILE_NAME);
        }
    }
}
