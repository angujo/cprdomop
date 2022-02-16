using DatabaseProcessor;
using System;
using System.IO;
using System.Reflection;
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

        public DBSchema Schema { get { return targetSchema; } }

        protected string DBMSName { get { return DBMSIdentifier.GetName(DBMSType.POSTGRESQL); } }
        public Script(DBSchema source, DBSchema target, DBSchema vocabulary)
        {
            sourceSchema = source;
            targetSchema = target;
            vocSchema = vocabulary;
        }

        public string AddIn(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string CareSite() { return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string CdmSource(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string ChunkLoad(int chunk, int limit, int offset)
        {
            chunkId = chunk;
            return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase())
                .Replace(@"{lmt}", $"{limit}")
                .Replace(@"{ost}", $"{offset}");
        }

        public string ChunkSetup() { return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string CohortDefinition() { return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string ConditionEra(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string ConditionOccurrence(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string CreateTables() { return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string Death(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string DeviceExposure(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string DrugEra(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string DrugExposure(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string Location() { return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string Measurement(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string ObservationPeriod(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string Observation(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string Person(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string ProcedureExposure(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string Provider() { return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string SourceToSource() { return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string SourceToStandard() { return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string Specimen(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string StemTable(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string TestInt(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string VisitDetail(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        public string VisitOccurrence(int chunk) { chunkId = chunk; return SQLScript(MethodBase.GetCurrentMethod().Name.ToKebabCase()); }

        private string SQLScript(string name) { return FileScript($"{name}.sql"); }

        private string FileScript(string file_name)
        {
            var filePath = File.Exists(file_name) ? file_name : Path.Combine(Environment.CurrentDirectory, "omopscripts", DBMSName, file_name);
            return File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
        }

        private string SetPlaceHolders(string script)
        {
            return script
                .Replace("{ch}", $"{chunkId}")
                .Replace("{vs}", vocSchema.SchemaName)
                .Replace("{ss}", sourceSchema.SchemaName)
                .Replace("{sc}", targetSchema.SchemaName);
        }
    }
}
