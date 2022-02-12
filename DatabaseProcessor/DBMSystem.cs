using DatabaseProcessor.postgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
