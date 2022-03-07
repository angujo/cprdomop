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
        protected int chunkId = -1;
        protected int limit = 0;

        protected readonly DBSchema sourceSchema;
        protected readonly DBSchema targetSchema;
        protected readonly DBSchema vocSchema;
        TimerLogger _cdmLogger;
        public AbsDBMSystem dBMSystem;

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

        public string SQLScript(string name, bool undo = false)
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
            var timer = SysDB<CDMTimer>.LoadOrNew("Where WorkLoadId = @WorkLoadId AND ChunkId = @ChunkId AND Name = @Name",
                new
                {
                    WorkLoadId = targetSchema.WorkLoadId,
                    ChunkId = chunkId,
                    Name = name,
                });
            timer.Status = Status.STARTED;
            timer.StartTime = DateTime.Now;
            timer.Query = query;
            timer.UpSert();
            try
            {
                // QueueTimer<CDMTimer>.Time(, name, () =>                {
                Logger.Info($"Start Running Query : {name} Chunk ID #{chunkId}");
                dBMSystem.RunQuery(query);
                //  _cdmLogger.updateTimer(chunkId, name, Status.COMPLETED);
                Logger.Info($"Done Running Query : {name} Chunk ID #{chunkId}");
                timer.Status = Status.COMPLETED;
            }
            catch (Exception ex)
            {
                timer.Status = Status.ERROR_EXIT;
                timer.ErrorLog = ex.Message;
                Logger.Exception(ex);
                throw;
            }
            finally
            {
                timer.EndTime = DateTime.Now;
                timer.UpSert();
            }

            //    }, new { WorkLoadId = targetSchema.WorkLoadId, ChunkId = chunkId, Query = query, Name = name });

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
                .Replace(@"{ch}", $"{chunkId}")
                .Replace(@"{vs}", null != vocSchema ? vocSchema.SchemaName : String.Empty)
                .Replace(@"{ss}", null != sourceSchema ? sourceSchema.SchemaName : String.Empty)
                .Replace(@"{sc}", targetSchema.SchemaName) //We must always have target schema, otherwise kill everything
                .Replace(@"{lmt}", $"{limit}");
        }
    }
}
