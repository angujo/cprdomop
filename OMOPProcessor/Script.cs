using System.Reflection;
using SystemLocalStore.models;

namespace OMOPProcessor
{
    public class Script : AbsCDMQuery
    {

        public Script(DBSchema source, DBSchema target, DBSchema vocabulary, TimerLogger logger) : base(source, target, vocabulary, logger) { }

        public void ChunkLoad(int lmt)
        {
            limit = lmt;
            RunLogTimer(MethodBase.GetCurrentMethod().Name);

            string from = @"COPY (select row_number() over(order by patid) rid, patid  from {ss}.patient p) TO STDOUT (FORMAT BINARY)";
            string to = @"COPY {sc}._chunk (ordinal, patient_id) FROM STDIN (FORMAT BINARY)";

            dBMSystem.GetType().GetMethod("BinaryCopy").Invoke(null, new object[] { sourceSchema, targetSchema, SetPlaceHolders(from), SetPlaceHolders(to) });

            dBMSystem.RunQuery(SetPlaceHolders("update {sc}._chunk set ordinal =q.ord from (select ceil((t.rid-1)/{lmt}) ord, t.patid  from (select row_number() over(order by p.patient_id) rid, patient_id patid from {sc}._chunk p) t) q where patient_id =q.patid "));
        }

        public void AddIn(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void CareSite() { RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void CdmSource() { RunLogTimer(MethodBase.GetCurrentMethod().Name); }
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
        public void StemAdditional(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void StemClinical(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void StemImmunization(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void StemRefferal(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void StemTest(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void StemTherapy(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        // public void StemTable(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void TestInt(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void VisitDetail(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void VisitOccurrence(int chunk) { chunkId = chunk; RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void VisitDetailUpdate() { RunLogTimer(MethodBase.GetCurrentMethod().Name); }
        public void VisitOccurrenceUpdate() { RunLogTimer(MethodBase.GetCurrentMethod().Name); }


    }
}
