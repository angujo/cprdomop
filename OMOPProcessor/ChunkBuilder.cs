using DatabaseProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore;
using SystemLocalStore.models;

namespace OMOPProcessor
{
    internal class ChunkBuilder
    {
        readonly Script script;
        private readonly AbsDBMSystem dBMSystem;

        public ChunkBuilder(Script s)
        {
            script = s;
            dBMSystem = DBMSystem.GetDBMSystem(script.Schema);
        }

        public Task Run(ChunkTimer chunk)
        {
            List<Action> actions = new List<Action>
                {
                    async ()=>{
                        await  new StemTableBuilder(chunk.ChunkId, script).Run();
                       await stemTableDependants(chunk);
                    },
                () => script.CdmSource(chunk.ChunkId),
                () => script.Death(chunk.ChunkId),
                () => script.ObservationPeriod(chunk.ChunkId),
                () => script.Observation(chunk.ChunkId),
                () => script.Person(chunk.ChunkId),
            };
            return Task.Run(() =>
            {
                QueueProcessor.Timed<ChunkTimer>(chunk.ChunkId,
                    () =>
                    {
                        Parallel.ForEach(actions, action => action());
                    },
                    new { Touched = true }, new { Touched = true });
            });
        }
        protected Task stemTableDependants(ChunkTimer chunk)
        {
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
            // Populate all stem_table_dependants
            return Task.Run(() => Parallel.ForEach(actions, task => task()));
        }

        public static Task Create(Script script) { return Task.Run(() => script.ChunkSetup()); }

        public Task Load(int limit) { return Task.Run(() => script.ChunkLoad(limit)); }
    }

}
