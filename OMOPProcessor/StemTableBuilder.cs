using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util;

namespace OMOPProcessor
{
    internal class StemTableBuilder
    {
        readonly int chunkId;
        readonly Script script;

        public StemTableBuilder(int index, Script s)
        {
            chunkId = index;
            script = s;
        }

        public void Run()
        {
            List<Action> tasks = new List<Action>
                {
                  /* () => // Populate visit_occurrence
                    {
                        script.VisitDetail(chunkId);
                        script.VisitOccurrence(chunkId);
                    },*/
                   () => script.AddIn(chunkId),
                   () => script.TestInt(chunkId),
                };
            Logger.Info($"Start StemTable Preparation ChunkID #{chunkId}");
            Parallel.ForEach(tasks, tsk => tsk());
            Logger.Info($"Start StemTable Series ChunkID #{chunkId}");
            script.StemAdditional(chunkId);
            script.StemClinical(chunkId);
            script.StemImmunization(chunkId);
            script.StemRefferal(chunkId);
            script.StemTest(chunkId);
            script.StemTherapy(chunkId);
            // script.StemTable(chunkId);
            Logger.Info($"Ended StemTable Series ChunkID #{chunkId}");
        }
    }
}
