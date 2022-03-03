using Util;

namespace DatabaseProcessor
{
    public enum DBMSType
    {
        [StringValue("postgres")]
        POSTGRESQL,
        [StringValue("mysql")]
        MYSQL,
        [StringValue("sqlite")]
        SQLITE,
    }

    public static class DBMSIdentifier
    {
        public static string GetName(DBMSType type)
        {
            switch (type)
            {
                case DBMSType.POSTGRESQL: return "postgres";
                case DBMSType.MYSQL: return "mysql";
                default: return string.Empty;
            }
        }

        public static string Active()
        {
            //TODO Add more DBMSs and auto switch
            return DBMSType.POSTGRESQL.GetStringValue();
        }
    }
}
