namespace DatabaseProcessor
{
    public enum DBMSType
    {
        POSTGRESQL,
        MYSQL
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
    }
}
