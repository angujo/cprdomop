using Npgsql;
using System;
using System.Collections.Generic;
using SystemLocalStore.models;
using Util;

namespace DatabaseProcessor.postgres
{
    public class PostgreSql : AbsDBMSystem
    {
        public PostgreSql(DBSchema db_schema) : base(db_schema) { }

        public override string Name { get { return "pgsql"; } }

        protected override string ConnectionString()
        {
            return String.Join(";", new string[] {
                @"Server="+ schema.Server,
                @"Port="+ schema.Port,
                @"User Id="+ schema.Username,
                @"Password="+ schema.Password,
                @"Database="+ schema.DBName,
                @"ApplicationName=OMOPBuilder",
                @"Pooling=false",
                @"IncludeErrorDetail=true",
                @"CommandTimeout=36000"
            });
        }

        public override dynamic GetConnection()
        {
            return new NpgsqlConnection(ConnectionString());
        }

        public override List<object> RunDataSet(string sql, object parameters = null)
        {
            List<object> rows = new List<object>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    prepareCommand(cmd, parameters);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object[] columns = new object[reader.FieldCount];
                            reader.GetValues(columns);
                            rows.Add(columns);
                        }
                    }
                }
            }
            return rows;
        }
        public override List<T> RunColumn<T>(string sql, object parameters = null)
        {
            List<T> rows = new List<T>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    prepareCommand(cmd, parameters);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rows.Add((T)Convert.ChangeType(reader.GetValue(0), typeof(T)));
                        }
                    }
                }
            }
            return rows;
        }

        public override void RunFile(string path)
        {
            throw new NotImplementedException();
        }

        public override async void RunQuery(string sql)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(sql);
                Console.WriteLine(ex.ToString());
                Logger.Error(sql);
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
                throw;
            }
        }

        public override void RunQueue(Queue queue)
        {
            throw new NotImplementedException();
        }

        public override T RunScalar<T>(string sql, object parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    prepareCommand(cmd, parameters);
                    return (T)cmd.ExecuteScalar();
                }
            }
        }

        public override bool TestConnection()
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                conn.Open();
                return conn.State == System.Data.ConnectionState.Open;
            }
        }
    }
}
