using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using SystemLocalStore.models;
using Util;

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

        public static T Load<T>(string table_name, Object parameters = null)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                var whereClause = new WhereClause(parameters);
                return cnn.QuerySingleOrDefault<T>($"select * from {table_name} {whereClause.AsString} LIMIT 1", whereClause.DynamicParameters);
            }
        }

        public static T Scalar<T>(string column_name, string table_name, Object parameters = null)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                var whereClause = new WhereClause(parameters);
                return cnn.QuerySingleOrDefault<T>($"select {column_name} from {table_name} {whereClause.AsString} LIMIT 1", whereClause.DynamicParameters);
            }
        }

        public static bool Exists(string table_name, Object parameters = null)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                var scalar = Scalar<int>("1", table_name, parameters);
                return null != scalar && 1 == scalar;
            }
        }

        public static List<T> LoadList<T>(string table_name, Object parameters = null, int? limit = null, int? offset = null)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                var whereClause = new WhereClause(parameters);
                var _limit = (null != limit ? $"LIMIT {limit}" + (null != offset ? $"OFFSET {offset}" : string.Empty) : string.Empty);
                return cnn.Query<T>($"select * from {table_name} {whereClause.AsString} {_limit}", whereClause.DynamicParameters).ToList();
            }
        }

        public static T Upsert<T>(T tblObject) where T : AbsUpsTable
        {
            if (!tblObject.GetType().IsSubclassOf(typeof(AbsUpsTable))) return tblObject;
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                var columns = ((AbsTable)tblObject).FillableColumns()
                    .Where(clm =>
                    {
                        PropertyInfo pi = tblObject.GetType().GetProperty(clm);
                        if (null == pi || "id".Equals(clm.ToLower())) return false;
                        var val = pi.GetValue(tblObject, null);
                        return null != val && val != val.GetDefault();
                    })
                    .ToArray();
                if (columns.Length <= 0) columns = new string[] { "Id" };
                var sql = $"INSERT INTO {tblObject.TableName()} ({string.Join(", ", columns)}) " +
                            $"VALUES({string.Join(", ", columns.Select(c => $"@{c}"))}) " +
                            $"ON CONFLICT({string.Join(", ", ((string[])tblObject.GetType().GetMethod("UpsColumns").Invoke(null, null)).Select(c => c))}) " +
                            $"DO UPDATE SET {string.Join(", ", columns.Select(c => $"{c} = @{c}").ToArray())}; ";
                if (tblObject.TableName().Equals("WorkQueue"))
                {
                    var aSql = $"UpsertQuery: {sql}";
                    var props = tblObject.GetType().GetProperties();
                    foreach (var prop in props)
                    {
                        aSql = aSql.Replace($"@{prop.Name}", $"'{prop.GetValue(tblObject)}'");
                    }
                    Logger.Info(aSql);
                    // throw new Exception(sql);
                    // Logger.Info(Environment.StackTrace);
                }

                var r = cnn.Execute((string)sql, (object)tblObject);
                tblObject.Id = cnn.QuerySingle<int>($"SELECT seq FROM sqlite_sequence WHERE name = @Name", new { Name = tblObject.TableName() });

                return (T)tblObject;
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
                if (null == parameters) return false;
                if (parameters.GetType().IsSubclassOf(typeof(AbsTable))) { parameters = new { Id = ((AbsTable)parameters).Id }; }
                var whereClause = new WhereClause(parameters);
                return 0 < cnn.Execute($"DELETE FROM {table_name} {whereClause.AsString}", whereClause.DynamicParameters);
            }
        }

        public static void LockWorkLoad(WorkLoad workLoad)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                cnn.Execute("UPDATE WorkLoad SET FilesLocked = 1  WHERE Id = @Id", workLoad);
            }
        }
        public static WorkQueue ForQueues(Status status)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString()))
            {
                return cnn.QuerySingleOrDefault<WorkQueue>("select * from WorkQueue where Status = @Stats ORDER BY Id ASC LIMIT 1", new { Stats = status });
            }
        }

        private static string connectionString()
        {
            return "Data Source=" + Setting.DBFilePath + ";Version=3;";
            //return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }

    public class WhereClause
    {
        ABSWhere absw;

        public string AsString { get { return absw.Wheres.Length == 0 ? string.Empty : $"WHERE {absw.WhereText}"; } }
        public DynamicParameters DynamicParameters { get { return absw.CompiledParams; } }

        public WhereClause(object _params)
        {
            absw = new ABSWhere(_params);
        }
    }

    public class G
    {
        protected string _opr = "AND";
        protected object _params;

        internal string Operand { get { return _opr; } }
        internal object Parameters { get { return _params; } }
        protected G() { }

        public static G Or(object parameters) { return process(parameters, "OR"); }

        public static G And(object parameters) { return process(parameters, "AND"); }

        internal static G process(object parameters, string opr)
        {
            return new G
            {
                _params = parameters,
                _opr = opr
            };
        }
    }

    internal class ABSWhere
    {
        string[] _wheres;
        List<string> _cols = new List<string>();
        DynamicParameters _props = new DynamicParameters();

        public string SelectColumns { get { return string.Join(", ", Columns); } }
        public string WhereText { get { return string.Join(" ", _wheres); } }
        public string[] Columns { get { return _cols.Distinct().ToArray(); } }
        public string[] Wheres { get { return _wheres.ToArray(); } }
        public DynamicParameters CompiledParams { get { return _props; } }

        public ABSWhere(object parameters) { _wheres = Process(parameters).ToArray(); }

        public static ABSWhere Load(object pars) { return new ABSWhere(pars); }

        /*
         * public ABSWhere process(object parameters)
        {
            Type t = parameters.GetType();
            if (t.IsArray)
            {
                var _parameters = (object[])parameters;
                foreach (object o in _parameters)
                {
                    if (o.GetType().Name.Equals(typeof(G)))
                    {
                        var param = (G)o;
                    }
                }
            }
            else
            {
                foreach (var pi in parameters.GetType().GetProperties())
                {
                    var cls = propertyInf(pi);
                    if (cls == null) continue;
                }
            }
            return this;
        }*/

        private List<string> Process(object entries)
        {
            List<string> ws = new List<string>();
            if (entries.GetType().IsArray)
            {
                var ps = (object[])entries;
                foreach (object o in ps)
                {
                    var opr = "AND";
                    List<string> _ws;
                    if (o.GetType().Name.Equals(typeof(G).Name))
                    {
                        var g = (G)o;
                        opr = g.Operand;
                        _ws = Process(g.Parameters);
                    }
                    else _ws = Process(o);
                    var c = _ws.Count;
                    if (c <= 0) continue;
                    if (ws.Count > 0) ws.Add(opr);
                    if (c > 1) ws.Add("(");
                    ws.AddRange(_ws);
                    if (c > 1) ws.Add(")");
                }
            }
            else if (!"System".Equals(entries.GetType().Namespace))
            {
                var props = entries.GetType().GetProperties();
                foreach (var pi in props)
                {
                    if (pi == null) continue;
                    var cls = propertyInf(entries, pi);
                    if (cls == null) continue;
                    if (ws.Count > 0) ws.Add("AND");
                    ws.Add(cls);
                }
            }
            return ws;
        }

        protected string propertyInf(object obj, PropertyInfo pinf)
        {
            var v = pinf.GetValue(obj);
            _cols.Add(pinf.Name);
            _props.Add(pinf.Name, v);
            var p = v.GetType().IsArray ? "IN" : "=";
            return $"{pinf.Name} {p} @{pinf.Name}";
        }
    }
}
