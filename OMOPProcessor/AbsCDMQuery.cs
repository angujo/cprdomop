using DatabaseProcessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace OMOPProcessor
{
    public abstract class AbsCDMQuery
    {
        protected int chunkId = 0;
        protected int limit = 0;

        readonly DBSchema sourceSchema;
        readonly DBSchema targetSchema;
        readonly DBSchema vocSchema;
        AbsDBMSystem dBMSystem;

        public DBSchema Schema { get { return targetSchema; } }

        protected string DBMSName { get { return DBMSIdentifier.GetName(DBMSType.POSTGRESQL); } }

        public AbsCDMQuery(DBSchema schema)
        {
            targetSchema = schema;
            dBMSystem = DBMSystem.GetDBMSystem(targetSchema);
        }

        public AbsCDMQuery(DBSchema source, DBSchema target, DBSchema vocabulary)
            : this(target)
        {
            sourceSchema = source;
            vocSchema = vocabulary;
        }

        private string SQLScript(string name)
        {
            return SetPlaceHolders(FileScript($"{name.ToKebabCase()}.sql"));
        }

        private string FileScript(string file_name)
        {
            var filePath = file_name;
            var nm = File.Exists(file_name) ? string.Empty : this.GetType().Name.ToLower();
            switch (nm)
            {
                case "script":
                    filePath = Path.Combine(Environment.CurrentDirectory, "omopscripts", DBMSName, file_name);
                    break;
                case "analyzer":
                    filePath = Path.Combine(Environment.CurrentDirectory, "omopscripts", DBMSName, "analysis", file_name);
                    break;
            }
            var cnt = File.Exists(filePath) ?  File.ReadAllText(filePath) : string.Empty;
            if (cnt.Length <= 0) throw new Exception($"Empty Content FOR FILE [{file_name} # {filePath}]");
            return cnt;
        }

        protected void RunLogTimer(string name)
        {
            var query = SQLScript(name);
            if (query.Length <= 0) return;
            QueueTimer<CDMTimer>.Time(name, () =>
            {
                dBMSystem.RunQuery(query);
            }, new { WorkLoadId = targetSchema.WorkLoadId, ChunkId = chunkId, Query = query, Name = name });
        }

        protected List<object> RunData(string name)
        {
            return dBMSystem.RunDataSet(SQLScript(name));
        }

        protected List<T> RunColumn<T>(string name)
        {
            return dBMSystem.RunColumn<T>(SQLScript(name));
        }

        protected T RunScalar<T>(string name)
        {
            return dBMSystem.RunScalar<T>(SQLScript(name));
        }

        protected string SetPlaceHolders(string script)
        {
            return script
                .Replace("{ch}", $"{chunkId}")
                .Replace("{vs}", null != vocSchema ? vocSchema.SchemaName : String.Empty)
                .Replace("{ss}", null != sourceSchema ? sourceSchema.SchemaName : String.Empty)
                .Replace("{sc}", targetSchema.SchemaName) //We must always have target schema, otherwise kill everything
                .Replace(@"{lmt}", $"{limit}");
        }
    }
}
