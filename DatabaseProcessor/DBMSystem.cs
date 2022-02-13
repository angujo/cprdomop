using DatabaseProcessor.postgres;
using SystemLocalStore.models;

namespace DatabaseProcessor
{
    public static class DBMSystem
    {
        public static AbsDBMSystem GetDBMSystem(DBSchema schema, DBMSType type = DBMSType.POSTGRESQL)
        {
            switch (type)
            {
                case DBMSType.POSTGRESQL: return new PostgreSql(schema);
                default: return null;
            }
        }
    }
}
