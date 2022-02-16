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

        public static T Load<T>(string table_name, Object parameters = null)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                var wheres=string.Empty;
                if(parameters != null)wheres = "WHERE " + string.Join(" AND ", parameters.GetType().GetProperties().Select(pi => $"{pi.Name} = @{pi.Name}").ToArray());
                parameters = null == parameters ? new DynamicParameters() : parameters;
                return cnn.QuerySingleOrDefault<T>($"select * from {table_name} {wheres} LIMIT 1", parameters);
            }
        }

        public static List<T> LoadList<T>(string table_name, Object parameters = null, int? limit = null, int? offset = null)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                var empty = null == parameters;
                parameters = empty ? new DynamicParameters() : parameters;
                var wheres = empty ? string.Empty : string.Join(" AND ", parameters.GetType().GetProperties().Select(pi => $"{pi.Name} = @{pi.Name}").ToArray());
                var _limit = (null != limit ? $"LIMIT {limit}" : string.Empty) + (null != offset ? $"OFFSET {offset}" : string.Empty);
                return cnn.Query<T>($"select * from {table_name} {(wheres.Length > 0 ? ("WHERE " + wheres) : string.Empty)} {_limit}", parameters).ToList();
            }
        }

        public static Int64? InsertOrUpdate(Object tblObject, bool loadInsertId = true)
        {
            if (!tblObject.GetType().IsSubclassOf(typeof(AbsTable))) return null;
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                var sql = "";
                var absTable = (AbsTable)tblObject;
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

        public static bool Delete(string table_name, object parameters = null)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                if (parameters.GetType().IsSubclassOf(typeof(AbsTable))) { parameters = new { Id = ((AbsTable)parameters).Id }; }
                var empty = null == parameters;
                parameters = empty ? new DynamicParameters() : parameters;
                var wheres = empty ? string.Empty : string.Join(" AND ", parameters.GetType().GetProperties().Select(pi => $"{pi.Name} = @{pi.Name}").ToArray());
                return 0 < cnn.Execute($"DELETE FROM {table_name} {(wheres.Length > 0 ? ("WHERE " + wheres) : string.Empty)}", parameters);
            }
        }

        public static void LockWorkLoad(WorkLoad workLoad)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                cnn.Execute("UPDATE WorkLoad SET FilesLocked = 1  WHERE Id = @Id", workLoad);
            }
        }
        public static WorkQueue ForQueues()
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                return cnn.QuerySingleOrDefault<WorkQueue>("select * from WorkQueue where Status IN @Stats ORDER BY Id ASC", new { Stats = new int[] { (int)Status.PAUSED, (int)Status.STOPPED, (int)Status.QUEUED } });
            }
        }

        private static string connectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
