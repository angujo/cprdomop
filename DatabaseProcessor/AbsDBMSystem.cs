using Npgsql;
using System.Collections.Generic;
using SystemLocalStore.models;

namespace DatabaseProcessor
{
    public abstract class AbsDBMSystem
    {
        protected DBSchema schema;
        public AbsDBMSystem(DBSchema db_schema) { schema = db_schema; }
        public abstract string Name { get; }
        public abstract dynamic GetConnection();
        protected abstract string ConnectionString();
        public abstract bool TestConnection();
        public abstract void RunFile(string path);
        public abstract void RunQuery(string sql);
        public abstract T RunScalar<T>(string sql, object parameters = null);
        public abstract List<T> RunColumn<T>(string sql, object parameters = null);
        public abstract List<object> RunDataSet(string sql, object parameters = null);
        public abstract void RunQueue(Queue queue);

        protected void prepareCommand(NpgsqlCommand cmd, object parameters = null)
        {
            if (null == parameters) return;
            var properties = parameters.GetType().GetProperties();
            if (properties.Length <= 0) return;
            foreach (var property in properties)
                cmd.Parameters.AddWithValue(property.Name, property.GetValue(parameters));
            cmd.Prepare();
        }
    }
}
