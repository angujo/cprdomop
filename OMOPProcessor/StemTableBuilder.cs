using DatabaseProcessor;
using System;
using System.Collections.Generic;
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

        public void Run()
        {
            List<Action> tasks = new List<Action>
                {
                   () => // Populate visit_occurrence
                    {
                        script.VisitDetail(chunkId);
                        script.VisitOccurrence(chunkId);
                        /*script.VisitOccurrenceUpdate(chunkId);
                        script.VisitDetailUpdate(chunkId);*/
                    },
                   () => script.AddIn(chunkId),
                   () => script.TestInt(chunkId),
                };
            Console.WriteLine("Start StemTable Preparation");
            Parallel.ForEach(tasks, tsk => tsk());
            Console.WriteLine("Start StemTable Series");
            script.StemTable(chunkId);
            Console.WriteLine("Ended StemTable Series");
        }
    }
}
