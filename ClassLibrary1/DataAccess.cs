using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStorage.models;

namespace SystemStorage
{
    public class DataAccess
    {
        public static List<SourceFile> loadSourceFiles(WorkLoad workLoad)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                return cnn.Query<SourceFile>("select * from SourceFile where WorkLoadId = @Id", workLoad).ToList();
            }
        }
        public static List<DBSchema> loadSchemas(WorkLoad workLoad)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                return cnn.Query<DBSchema>("select * from DBSchema where WorkLoadId = @Id", workLoad).ToList();
            }
        }
        public static DBSchema loadSchema(WorkLoad workLoad, string s_type)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                return cnn.QuerySingleOrDefault<DBSchema>("select * from DBSchema where WorkLoadId = @Id AND SchemaType = @SchemaType", new { Id = workLoad.Id, SchemaType = s_type });
            }
        }

        public static List<WorkLoad> loadWorkLoads()
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                return cnn.Query<WorkLoad>("select * from WorkLoad", new DynamicParameters()).ToList();
            }
        }

        public static Int64 InsertOrUpdate(AbsTable absTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                var sql = "";
                var columns = absTable.FillableColumns().ToArray();
                if (0 < absTable.Id) sql = $"UPDATE {absTable.TableName()} SET {string.Join(", ", columns.Select(c => $"{c} = @{c}").ToArray())} WHERE Id = @Id";
                else sql = $"insert into {absTable.TableName()} ({string.Join(", ", columns)}) values({string.Join(", ", columns.Select(c => $"@{c}"))})";
                Console.WriteLine(sql);
                var r = cnn.Execute(sql, absTable);
                if (0 >= absTable.Id) absTable.Id = r;
                return absTable.Id;
            }
        }

        public static void CleanSourceFiles(WorkLoad workLoad)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                cnn.Execute("UPDATE WorkLoad SET FilesLocked = 0  WHERE Id = @Id", workLoad);
                cnn.Execute("DELETE FROM SourceFile WHERE WorkLoadId = @Id", workLoad);
            }
        }

        public static void LockWorkLoad(WorkLoad workLoad)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                cnn.Execute("UPDATE WorkLoad SET FilesLocked = 1  WHERE Id = @Id", workLoad);
            }
        }

        private static string connectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
