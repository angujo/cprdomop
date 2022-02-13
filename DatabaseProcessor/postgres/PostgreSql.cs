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

        public override void RunQuery(string sql)
        {
            throw new NotImplementedException();
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
