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
                await Task.Run(() => // Populate Stem Table dependencies for the chunk i.e. visit_detail, visit_occurrence, add_in, test_int
                {
                    Task.Run(() => // Populate visit_occurrence
                    {
                        dBMSystem.RunQuery(script.VisitDetail(chunkId));
                        dBMSystem.RunQuery(script.VisitOccurrence(chunkId));
                    });
                    Task.Run(() => dBMSystem.RunQuery(script.AddIn(chunkId)));
                    Task.Run(() => dBMSystem.RunQuery(script.TestInt(chunkId)));
                });
                dBMSystem.RunQuery(script.StemTable(chunkId));
            });

        }
    }
}
