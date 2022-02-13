using SystemLocalStore.models;

namespace DatabaseProcessor
{
    public abstract class AbsDBMSystem
    {
        protected DBSchema schema;
        public AbsDBMSystem(DBSchema db_schema) { schema = db_schema; }
        public abstract string Name { get; }
        public abstract dynamic GetConnection();
        public abstract string ConnectionString();
        public abstract bool TestConnection();
        public abstract void RunFile(string path);
        public abstract void RunQuery(string sql);
        public abstract void RunQueue(Queue queue);
    }
}
