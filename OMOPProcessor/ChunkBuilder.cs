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
            int reruns = 11;
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
                () => script.Death(chunk.ChunkId),
                () => script.ObservationPeriod(chunk.ChunkId),
                () => script.Observation(chunk.ChunkId),
                () => script.Person(chunk.ChunkId),
            };
        RunChunk:
            Logger.Info($"Start Chunk Series ChunkID#{chunk.ChunkId}");
            Parallel.ForEach(actions, action => action());
            Logger.Info($"Ended Chunk Series ChunkID#{chunk.ChunkId}");
            if (SysDB<CDMTimer>.Exists("Where WorkLoadId = @WLId AND ChunkId = @CHId AND Status <> 8", new { WLId = chunk.WorkLoadId, CHId = chunk.ChunkId, }))
            {
                reruns--;
                if (reruns <= 0)
                {
                    chunk.ErrorLog = $"Too Many Re-Runs for ChunkId#{chunk.ChunkId} WL#{chunk.WorkLoadId}";
                    chunk.Status = Status.ERROR_EXIT;
                    chunk.Save();
                    return;
                }
                else
                {
                    Logger.Info($"ChunkID#{chunk.ChunkId} timers are still lagging. Starting Re-Run#{reruns}!");
                Task.Delay(200);
                    goto RunChunk;
                }
            }
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
                    () => script.Measurement(chunk.ChunkId),
                    () => script.ProcedureExposure(chunk.ChunkId),
                    () => script.Specimen(chunk.ChunkId)
                };
            Parallel.ForEach(actions, task => task());
            Logger.Info($"Done With StemTables Dependants! ChunkID#{chunk.ChunkId}");
        }

        public static void Create(Script script) { script.ChunkSetup(); }

        public void Load(int limit) { script.ChunkLoad(limit); }
    }

}
