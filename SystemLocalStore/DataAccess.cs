using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using SystemLocalStore.models;

namespace SystemLocalStore
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
                return cnn.QuerySingleOrDefault<DBSchema>("select * from DBSchema where WorkLoadId = @Id AND SchemaType = @SchemaType LIMIT 1", new { Id = workLoad.Id, SchemaType = s_type });
            }
        }

        public static List<WorkLoad> loadWorkLoads()
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                return cnn.Query<WorkLoad>("select * from WorkLoad", new DynamicParameters()).ToList();
            }
        }

        public static Int64 InsertOrUpdate(AbsTable absTable, bool loadInsertId = true)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                var sql = "";
                var columns = absTable.FillableColumns().ToArray();

                if (absTable.Exists()) sql = $"UPDATE {absTable.TableName()} SET {string.Join(", ", columns.Select(c => $"{c} = @{c}").ToArray())} WHERE Id = @Id;";
                else sql = $"insert into {absTable.TableName()} ({string.Join(", ", columns)}) values({string.Join(", ", columns.Select(c => $"@{c}"))});";

                var r = cnn.Execute(sql, absTable);
                if (loadInsertId && !absTable.Exists())
                {
                    r = cnn.QuerySingle<int>($"SELECT seq FROM sqlite_sequence WHERE name = @Name", new { Name = absTable.TableName() });
                    absTable.Id = r;
                }

                return r;
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

        public static bool Delete(AbsTable absTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                return 0 < cnn.Execute($"DELETE FROM {absTable.TableName()} WHERE Id = @Id", absTable);
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
