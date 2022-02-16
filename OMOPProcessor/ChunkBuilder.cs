using DatabaseProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMOPProcessor
{
    internal class ChunkBuilder
    {
        readonly int chunkId;
        readonly Script script;
        private readonly AbsDBMSystem dBMSystem;

        public ChunkBuilder(int index, Script s)
        {
            chunkId = index;
            script = s;
            dBMSystem = DBMSystem.GetDBMSystem(script.Schema);
        }

        public Task Run()
        {
            List<Action> actions = new List<Action>
                {
                    async ()=>{
                        await  new StemTableBuilder(chunkId, script).Run();
                        stemTableDependants();
                    },
                () => script.CdmSource(chunkId),
                () => script.Death(chunkId),
                () => script.ObservationPeriod(chunkId),
                () => script.Observation(chunkId),
                () => script.Person(chunkId),
            };
            return Task.Run(() => Parallel.ForEach(actions, action => action()));
        }
        protected Task stemTableDependants()
        {
            List<Action> actions = new List<Action>
                {
                    () =>{
                        script.ConditionOccurrence(chunkId);
                        script.ConditionEra(chunkId);
                    },
                    () => script.DeviceExposure(chunkId),
                    () =>{
                        script.DrugExposure(chunkId);
                        script.DrugEra(chunkId);
                    },
                    () => script.Measurement(chunkId),
                    () => script.ProcedureExposure(chunkId),
                    () => script.Specimen(chunkId)
                };
            // Populate all stem_table_dependants
            return Task.Run(() => Parallel.ForEach(actions, task => task()));
        }

        public static Task Create(Script script) { return Task.Run(() => script.ChunkSetup()); }

        public Task Load(int chunkId, int limit, int offset = 0) { return Task.Run(() => script.ChunkLoad(chunkId, limit, offset)); }
    }

}
