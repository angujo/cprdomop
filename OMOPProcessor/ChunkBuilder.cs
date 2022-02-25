using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
