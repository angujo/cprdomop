using DatabaseProcessor;
using System;
using System.Collections.Generic;
using System.IO;
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
        TimerLogger _cdmLogger;
        AbsDBMSystem dBMSystem;

        public TimerLogger CdmLogger { get { return _cdmLogger; } }

        public DBSchema Schema { get { return targetSchema; } }

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

        public AbsCDMQuery(DBSchema source, DBSchema target, DBSchema vocabulary, TimerLogger logger)
            : this(source, target, vocabulary)
        { _cdmLogger = logger; }

        private string SQLScript(string name, bool undo = false)
        {
            return SetPlaceHolders(FileScript($"{name.ToKebabCase()}.sql", undo));
        }

        private string FileScript(string file_name, bool undo = false)
        {
            var filePath = file_name;
            var nm = File.Exists(file_name) ? string.Empty : this.GetType().Name.ToLower();
            switch (nm)
            {
                case "script":
                    filePath = Path.Combine(Setting.InstallationDirectory, "omopscripts", DBMSIdentifier.Active(), undo ? "undo" : String.Empty, file_name);
                    break;
                case "analyzer":
                    filePath = Path.Combine(Setting.InstallationDirectory, "omopscripts", DBMSIdentifier.Active(), "analysis", file_name);
                    break;
            }
            var cnt = File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
            if (cnt.Length <= 0) throw new Exception($"Empty Content FOR FILE [{file_name} # {filePath}]");
            return cnt;
        }

        protected void RunLogTimer(string name)
        {
            var query = SQLScript(name);
            if (query.Length <= 0) return;
            if (null != _cdmLogger && !_cdmLogger.RunCDMTimer(name, chunkId, _name =>
                {
                    var uQuery = SQLScript(_name, true);
                    Logger.Info($"Start Running CleanUp Query : {_name} Chunk ID #{chunkId}");
                    Logger.Info($"CleanUp Query : {uQuery}");
                    dBMSystem.RunQuery(uQuery);
                    Logger.Info($"Done Running CleanUp Query : {_name} Chunk ID #{chunkId}");
                })) return;
            QueueTimer<CDMTimer>.Time(name, () =>
            {
                Logger.Info($"Start Running Query : {name} Chunk ID #{chunkId}");
                dBMSystem.RunQuery(query);
                _cdmLogger.updateTimer(chunkId, name, Status.COMPLETED);
                Logger.Info($"Done Running Query : {name} Chunk ID #{chunkId}");
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
