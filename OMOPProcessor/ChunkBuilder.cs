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

        public ChunkBuilder(Script s)
        {
            script = s;
        }

        public void Run(ChunkTimer chunk)
        {
            List<Action> actions = new List<Action>
                {
                     ()=>{
                        Console.WriteLine("Commencing With StemTables!");
                        new StemTableBuilder(chunk.ChunkId, script).Run();
                        stemTableDependants(chunk);
                        Console.WriteLine("Done With StemTables!");
                    },
                () => script.Death(chunk.ChunkId),
                () => script.ObservationPeriod(chunk.ChunkId),
                () => script.Observation(chunk.ChunkId),
                () => script.Person(chunk.ChunkId),
            };
            Console.WriteLine("Start Chunk Series");
            Parallel.ForEach(actions, action => action());
            Console.WriteLine("Ended Chunk Series");
        }
        protected void stemTableDependants(ChunkTimer chunk)
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
            Parallel.ForEach(actions, task => task());
        }

        public static void Create(Script script) {  script.ChunkSetup(); }

        public void Load(int limit) { script.ChunkLoad(limit); }
    }

}
