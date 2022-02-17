using DatabaseProcessor;
using System;
using System.IO;
using System.Reflection;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace OMOPProcessor
{
    public class Script:AbsCDMQuery
    {

        public Script(DBSchema source, DBSchema target, DBSchema vocabulary) : base(source, target, vocabulary)
        {
        }

        public void ChunkLoad(int lmt) { limit = lmt; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
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

        
    }
}
