using DatabaseProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMOPProcessor
{
    internal class StemTableBuilder
    {
        readonly int chunkId;
        readonly Script script;
        private readonly AbsDBMSystem dBMSystem;

        public StemTableBuilder(int index, Script s)
        {
            chunkId = index;
            script = s;
            dBMSystem = DBMSystem.GetDBMSystem(script.Schema);
        }

        public Task Run()
        {
            return Task.Run(async () => // Populate the stem table with chunk
            {
                List<Task> tasks = new List<Task>
                {
                    Task.Run(() => // Populate visit_occurrence
                    {
                        script.VisitDetail(chunkId);
                        script.VisitOccurrence(chunkId);
                        script.VisitOccurrenceUpdate(chunkId);
                        // script.VisitDetailUpdate(chunkId);
                    }),
                    Task.Run(() => script.AddIn(chunkId)),
                    Task.Run(() => script.TestInt(chunkId)),
                };
                Task.WaitAll(tasks.ToArray());
                script.StemTable(chunkId);
            });

        }
    }
}
