using Npgsql;
using System;
using SystemLocalStore.models;

namespace DatabaseProcessor.postgres
{
    public class PostgreSql : AbsDBMSystem
    {
        public PostgreSql(DBSchema db_schema) : base(db_schema) { }

        public override string Name { get { return "pgsql"; } }

        public override string ConnectionString()
        {
            return String.Join(";", new string[] {
                @"Server="+ schema.Server,
                @"Port="+ schema.Port,
                @"User Id="+ schema.Username,
                @"Password="+ schema.Password,
                @"Database="+ schema.DBName,
                @"ApplicationName=OMOPBuilder",
                @"Pooling=false",
                @"CommandTimeout=36000"
            });
        }

        public override dynamic GetConnection()
        {
            return new NpgsqlConnection(ConnectionString());
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
                throw ex;
            }
        }

        public override void RunQueue(Queue queue)
        {
            throw new NotImplementedException();
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
