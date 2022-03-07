using Dapper;
using Npgsql;
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
            return RunConnection(cnn =>
            {
                return cnn.GetList<SourceFile>("where WorkLoadId = @Id", workLoad).ToList();
            });
        }

        public static T Load<T>(string table_name, string condition = null, Object parameters = null)
        {
            return RunConnection(cnn =>
            {
                // var whereClause = new WhereClause(parameters);
                return cnn.QuerySingleOrDefault<T>($"select * from {table_name} {condition} LIMIT 1", parameters);
            });
        }

        public static T Scalar<T>(string column_name, string table_name, string condition = null, Object parameters = null)
        {
            return RunConnection(cnn =>
            {
                var whereClause = new WhereClause(parameters);
                var sql = $"select {column_name} from {table_name} {condition} LIMIT 1";
                return cnn.QuerySingleOrDefault<T>(sql, parameters);
            });
        }

        public static List<C> Column<C>(string column_name, string table_name, string condition = null, Object parameters = null)
        {
            using (IDbConnection cnn = connection())
            {
                var whereClause = new WhereClause(parameters);
                var sql = $"select {column_name} AS col from {table_name} {condition}";
                List<C> l = new List<C>();
                return cnn.Query(sql, parameters).Select(c => (C)c.col).ToList();
            };
        }

        public static bool Exists(string table_name, string condition = null, Object parameters = null)
        {
            var scalar = Scalar<int>("1", table_name, condition, parameters);
            return null != scalar && 1 == scalar;
        }

        public static List<T> LoadList<T>()
        {
            return RunConnection(cnn =>
            {
                return cnn.GetList<T>().ToList();
            });
        }

        public static int Query(string sql, object parameters = null)
        {
            return RunConnection(cnn =>
            {
                return cnn.Execute(sql, parameters);
            });
        }

        public static List<T> LoadList<T>(Object parameters = null)
        {
            return RunConnection(cnn =>
            {
                return cnn.GetList<T>(parameters).ToList();
            });
        }

        public static List<T> LoadList<T>(string condition = null, Object parameters = null, int? limit = null, int? offset = null)
        {
            return RunConnection(cnn =>
            {
                var cols = string.Join(", ", typeof(T).GetProperties().Select(p => p.Name).ToArray());
                return cnn.Query<T>($"SELECT {cols} FROM {typeof(T).Name} {condition}", parameters).ToList();
            });
        }

        public static T Upsert<T>(T tblObject) where T : AbsUpsTable
        {
            if (!tblObject.GetType().IsSubclassOf(typeof(AbsUpsTable))) return tblObject;

            return RunConnection(cnn =>
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
                            $"ON CONFLICT {UpsertConflict(tblObject)} " +
                            $"DO UPDATE SET {string.Join(", ", columns.Select(c => $"{c} = @{c}").ToArray())} " +
                            (Setting.DBConnection == 0 ? " RETURNING Id " : "");

                // Logger.Info(sql);
                var r = cnn.Execute((string)sql, (object)tblObject);
                if (!tblObject.Exists())
                {
                    tblObject.Id = Setting.DBConnection == 0 ? r : cnn.QuerySingle<int>($"SELECT seq FROM sqlite_sequence WHERE name = @Name", new { Name = tblObject.TableName() });
                }


                return (T)tblObject;
            });
        }

        private static string UpsertConflict<T>(T tblObject)
        {
            switch (Setting.DBConnection)
            {
                case 0:
                    var uidx = (string)tblObject.GetType().GetMethod("UpsIndex").Invoke(null, null);
                    return String.IsNullOrEmpty(uidx) ? String.Empty : $"ON CONSTRAINT {uidx}";
                default: return $"({string.Join(", ", ((string[])tblObject.GetType().GetMethod("UpsColumns").Invoke(null, null)).Select(c => c))}) ";
            }
        }

        public static Int64? InsertOrUpdate(AbsTable tblObject, bool loadInsertId = true)
        {
            return RunConnection<Int64?>(cnn =>
            {
                var sql = "";
                var absTable = (AbsTable)tblObject;
                var columns = absTable.FillableColumns().ToArray();
                var rets = Setting.DBConnection == 0 ? " RETURNING Id " : "";
                int r = 0;
                if (absTable.Exists())
                {
                    sql = $"UPDATE {absTable.TableName()} SET {string.Join(", ", columns.Select(c => $"{c} = @{c}").ToArray())} WHERE Id = @Id;";
                    //   cnn.Update(absTable);
                }
                else
                {
                    sql = $"insert into {absTable.TableName()} ({string.Join(", ", columns)}) values({string.Join(", ", columns.Select(c => $"@{c}"))}) {rets};";
                    // r = (int)cnn.Insert(absTable);
                }

                r = cnn.Execute(sql, absTable);
                if (loadInsertId && !absTable.Exists() && new int[] { 0, 2 }.Contains(Setting.DBConnection))
                {
                    if (2 == Setting.DBConnection)
                        r = cnn.QuerySingle<int>($"SELECT seq FROM sqlite_sequence WHERE name = @Name", new { Name = absTable.TableName() });
                    absTable.Id = r;
                }

                return r;
            });
        }

        public static int CleanSourceFiles(WorkLoad workLoad)
        {
            return RunConnection(cnn =>
            {
                cnn.Execute("UPDATE WorkLoad SET FilesLocked = 0  WHERE Id = @Id", workLoad);
                return cnn.Execute("DELETE FROM SourceFile WHERE WorkLoadId = @Id", workLoad);
            });
        }

        public static bool Delete(string table_name, string condition, object parameters)
        {
            return RunConnection(cnn =>
            {
                if (null == parameters && string.IsNullOrEmpty(condition)) return false;
                if (string.IsNullOrEmpty(condition))
                {
                    if (parameters.GetType().IsSubclassOf(typeof(AbsTable))) { parameters = new { Id = ((AbsTable)parameters).Id }; }
                    var whereClause = new WhereClause(parameters);
                    condition = whereClause.AsString;
                    parameters = whereClause.DynamicParameters;
                }
                return 0 < cnn.Execute($"DELETE FROM {table_name} {condition}", parameters);
            });
        }

        public static bool Delete(string table_name, object parameters = null)
        {
            return Delete(table_name, null, parameters);
        }

        public static int LockWorkLoad(WorkLoad workLoad)
        {
            return RunConnection(cnn =>
            {
                return cnn.Execute("UPDATE WorkLoad SET FilesLocked = 1  WHERE Id = @Id", workLoad);
            });
        }
        public static WorkQueue ForQueues(Status status)
        {
            return RunConnection(cnn =>
            {
                return cnn.QuerySingleOrDefault<WorkQueue>("select * from WorkQueue where Status = @Stats ORDER BY Id ASC LIMIT 1", new { Stats = status });
            });
        }

        private static T RunConnection<T>(Func<IDbConnection, T> act)
        {
            using (IDbConnection cnn = connection())
            {
                return act(cnn);
            }

        }

        private static IDbConnection connection()
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            var r = new SimpleCRUDResolver();
            SimpleCRUD.SetTableNameResolver(r);
            SimpleCRUD.SetColumnNameResolver(r);

            switch (Setting.DBConnection)
            {
                case 0:
                    return new NpgsqlConnection(
                String.Join(";", new string[] {
                @"Server="+ Setting.DBServer,
                @"Port="+ Setting.DBPort,
                @"User Id="+ Setting.DBUsername,
                @"Password="+ Setting.DBPassword,
                @"Database="+ Setting.DBName,
                @"ApplicationName=OMOPBuilder--Internal",
                @"Pooling=false",
                @"IncludeErrorDetail=true",
                @"CommandTimeout=36000"
        }));
                default: return new SQLiteConnection("Data Source=" + Setting.DBFilePath + ";Version=3;");
            }
        }

        public static DBSchema DBSchema()
        {
            return new DBSchema
            {
                DBName = Setting.DBName,
                Port = int.Parse(Setting.DBPort),
                SchemaName = Setting.DBSchema,
                Password = Setting.DBPassword,
                Username = Setting.DBUsername,
                Server = Setting.DBServer,
            };
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
        int _propsCount = 0;
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
            if (null == entries) return ws;
            if (entries.GetType().IsArray)
            {
                var ps = (object[])entries;
                foreach (object o in ps)
                {
                    if (null == o) continue;
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
            _propsCount++;
            var v = pinf.GetValue(obj);
            _cols.Add(pinf.Name);
            var arg = $"arg{_propsCount}";
            _props.Add(arg, v);
            var p = v.GetType().IsArray ? "IN" : "=";
            return $"{pinf.Name} {p} @{arg}";
        }
    }
}
