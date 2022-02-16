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
                () => dBMSystem.RunQuery(script.CdmSource(chunkId)),
                () => dBMSystem.RunQuery(script.Death(chunkId)),
                () => dBMSystem.RunQuery(script.ObservationPeriod(chunkId)),
                () => dBMSystem.RunQuery(script.Observation(chunkId)),
                () => dBMSystem.RunQuery(script.Person(chunkId)),
            };
            return Task.Run(() => Parallel.ForEach(actions, action => action()));
        }
        protected Task stemTableDependants()
        {
            List<Action> actions = new List<Action>
                {
                    () =>{
                        dBMSystem.RunQuery(script.ConditionOccurrence(chunkId));
                        dBMSystem.RunQuery(script.ConditionEra(chunkId));
                    },
                    () => dBMSystem.RunQuery(script.DeviceExposure(chunkId)),
                    () =>{
                        dBMSystem.RunQuery(script.DrugExposure(chunkId));
                        dBMSystem.RunQuery(script.DrugEra(chunkId));
                    },
                    () => dBMSystem.RunQuery(script.Measurement(chunkId)),
                    () => dBMSystem.RunQuery(script.ProcedureExposure(chunkId)),
                    () => dBMSystem.RunQuery(script.Specimen(chunkId))
                };
            // Populate all stem_table_dependants
            return Task.Run(() => Parallel.ForEach(actions, task => task()));
        }

        public static Task Create(Script script) { return Task.Run(() => DBMSystem.GetDBMSystem(script.Schema).RunQuery(script.ChunkSetup())); }

        public Task Load(int chunkId, int limit, int offset = 0) { return Task.Run(() => dBMSystem.RunQuery(script.ChunkLoad(chunkId, limit, offset))); }
    }

}
