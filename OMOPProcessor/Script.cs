using DatabaseProcessor;
using System;
using System.IO;
using System.Reflection;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace OMOPProcessor
{
    public class Script
    {
        readonly DBSchema sourceSchema;
        readonly DBSchema targetSchema;
        readonly DBSchema vocSchema;
        int chunkId = 0;
        int limit = 0;
        int offset = 0;

        AbsDBMSystem dBMSystem;

        public DBSchema Schema { get { return targetSchema; } }

        protected string DBMSName { get { return DBMSIdentifier.GetName(DBMSType.POSTGRESQL); } }
        public Script(DBSchema source, DBSchema target, DBSchema vocabulary)
        {
            sourceSchema = source;
            targetSchema = target;
            vocSchema = vocabulary;
            dBMSystem = DBMSystem.GetDBMSystem(sourceSchema);
        }

        public void ChunkLoad(int chunk, int lmt, int ofSt)
        {
            chunkId = chunk; limit = lmt; offset = ofSt;
            RunLogTimer(MethodBase.GetCurrentMethod().Name);
        }

        public void AddIn(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void CareSite() { RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void CdmSource(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void ChunkSetup() { RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void CohortDefinition() { RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void ConditionEra(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void ConditionOccurrence(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void CreateTables() { RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void Death(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void DeviceExposure(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void DrugEra(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void DrugExposure(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void Location() { RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void Measurement(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void ObservationPeriod(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void Observation(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void Person(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void ProcedureExposure(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void Provider() { RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void SourceToSource() { RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void SourceToStandard() { RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void Specimen(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void StemTable(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void TestInt(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void VisitDetail(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void VisitOccurrence(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }

        private string SQLScript(string name) { return SetPlaceHolders(FileScript($"{name.ToKebabCase()}.sql")); }

        private string FileScript(string file_name)
        {
            var filePath = File.Exists(file_name) ? file_name : Path.Combine(Environment.CurrentDirectory, "omopscripts", DBMSName, file_name);
            return File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
        }

        private void RunLogTimer(string name)
        {
            var query = SQLScript(name);
            QueueProcessor.Add<CDMTimer>(name, new { Name = name, ChunkId = chunkId, Query = query, StartTime = DateTime.Now });
            dBMSystem.RunQuery(query);
            QueueProcessor.Add<CDMTimer>(name, new { Name = name, ChunkId = chunkId, Query = query, EndTime = DateTime.Now });
        }

        private string SetPlaceHolders(string script)
        {
            return script
                .Replace("{ch}", $"{chunkId}")
                .Replace("{vs}", vocSchema.SchemaName)
                .Replace("{ss}", sourceSchema.SchemaName)
                .Replace("{sc}", targetSchema.SchemaName)
                .Replace(@"{lmt}", $"{limit}")
                .Replace(@"{ost}", $"{offset}");
        }
    }
}
