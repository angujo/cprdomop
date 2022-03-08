using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace OMOPProcessor
{
    internal class ChunkBuilder
    {
        readonly Script script;

        public ChunkBuilder(Script s)
        {
            script = s;
        }

        public void Run(ChunkTimer chunk)
        {
            Logger.Info($"Started Preparing CDM Timer Logger ChunkID#{chunk.ChunkId} Load WL#{chunk.WorkLoadId}");
            script.CdmLogger.prepareCDMTimers(chunk);
            Logger.Info($"Ended Preparation of CDM Timer Logger ChunkID#{chunk.ChunkId} Load WL#{chunk.WorkLoadId}");
            List<Action> actions = new List<Action>
            {
                ()=>{
                    Logger.Info($"Commencing With StemTables! ChunkID#{chunk.ChunkId}");
                    new StemTableBuilder(chunk.ChunkId, script).Run();
                    stemTableDependants(chunk);
                    Logger.Info($"Done With StemTables! ChunkID#{chunk.ChunkId}");
                },
                () => // Populate visit_occurrence
                {
                    script.VisitDetail(chunk.ChunkId);
                    script.VisitOccurrence(chunk.ChunkId);
                },
                () => script.Death(chunk.ChunkId),
                () => script.ObservationPeriod(chunk.ChunkId),
                () => script.Person(chunk.ChunkId),
            };
            Logger.Info($"Start Chunk Series ChunkID#{chunk.ChunkId}");
            Parallel.ForEach(actions, action => action());
            Logger.Info($"Ended Chunk Series ChunkID#{chunk.ChunkId}");
        }
        protected void stemTableDependants(ChunkTimer chunk)
        {
            Logger.Info($"Commencing With StemTables Dependants! ChunkID#{chunk.ChunkId}");
            List<Action> actions = new List<Action>
                {
                    () =>{
                        script.ConditionOccurrence(chunk.ChunkId);
                        script.ConditionEra(chunk.ChunkId);
                    },
                    () => script.DeviceExposure(chunk.ChunkId),
                    () =>{
                        script.DrugExposure(chunk.ChunkId);
                        script.DrugEra(chunk.ChunkId);
                    },
                    () => script.Observation(chunk.ChunkId),
                    () => script.Measurement(chunk.ChunkId),
                    () => script.ProcedureExposure(chunk.ChunkId),
                    () => script.Specimen(chunk.ChunkId)
                };
            Parallel.ForEach(actions, task => task());
            Logger.Info($"Done With StemTables Dependants! ChunkID#{chunk.ChunkId}");
        }

        public static void Create(Script script) { script.ChunkSetup(); }

        public void Load(int limit) { script.ChunkLoad(limit); }

        public void Clean(WorkLoad workLoad)
        {
            List<CDMTimer> timers = SysDB<CDMTimer>.List($"WHERE Status = 0 AND WorkLoadId = @WLId", new { WLId = workLoad.Id });
            foreach (var timer in timers)
            {
                script.CdmLogger.RunCDMTimer(timer.Name, timer.ChunkId, _name =>
                    {
                        var uQuery = script.SQLScript(_name, true);
                        Logger.Info($"Start Running CleanUp Query : {_name} Chunk ID #{timer.ChunkId}");
                        Logger.Info($"CleanUp Query : {uQuery}");
                        script.dBMSystem.RunQuery(uQuery);
                        Logger.Info($"Done Running CleanUp Query : {_name} Chunk ID #{timer.ChunkId}");
                    });
            }
        }
    }
}
