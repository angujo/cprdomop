using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest.localdb
{
    internal class StorageConnection
    {
        private SQLiteConnection conn;
        public static StorageConnection me;

        public StorageConnection()
        {
            conn = new SQLiteConnection("URI=file:./database.db");
        }

        public static void createTables()
        {
            init()
                .createFileReferenceTable();
        }

        private static StorageConnection init()
        {
            return me = me ?? new StorageConnection();
        }

        private StorageConnection createFileReferenceTable()
        {
            string q = "CREATE TABLE IF NOT EXISTS file_references(id INTEGER PRIMARY KEY, reference_id INTEGER, table_name TEXT, file_path TEXT);";
            conn.Open();
            using (var cmd = new SQLiteCommand(q, conn))
                cmd.ExecuteNonQuery(); return this;
        }
    }
}
