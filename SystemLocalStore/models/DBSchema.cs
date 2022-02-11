using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLocalStore.models
{
    public class DBSchema : AbsTable
    {
        public Int64 WorkLoadId { get { return getValue<Int64>("WorkLoadId"); } set { setValue("WorkLoadId",value); } }
        public string SchemaType { get { return getValue<string>("SchemaType"); } set { setValue("SchemaType", value); } }
        public int Port { get { return getValue<int>("Port"); } set { setValue("Port", value); } }
        public string Server { get { return getValue<string>("Server"); } set { setValue("Server", value); } }
        public string DBName { get { return getValue<string>("DBName"); } set { setValue("DBName", value); } }
        public string SchemaName { get { return getValue<string>("SchemaName"); } set { setValue("SchemaName", value); } }
        public string Username { get { return getValue<string>("Username"); } set { setValue("Username", value); } }
        public string Password { get { return getValue<string>("Password"); } set { setValue("Password", value); } }
    }
}
