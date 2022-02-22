using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore;
using SystemLocalStore.models;

namespace OMOPProcessor
{
    public class CDMBuilder
    {
        WorkLoad workLoad;
        DBSchema sourceSchema;
        DBSchema targetSchema;
        DBSchema vocabularySchema;
        Script script;
        Analyzer analyzer;

        public CDMBuilder(WorkLoad wl)
        {
            workLoad = wl;
            LoadSchemas();
        }

        public async Task RunAsync()
        {
            if (workLoad.CdmProcessed) return;
            Console.WriteLine($"Started Run for WL#{workLoad.Id}");
            await loadCdm();
            Console.WriteLine($"Prepare Chunk Load WL#{workLoad.Id}");
            await loadChunks();
            Console.WriteLine($"Start Cleaner WL#{workLoad.Id}");
            await loadCleaner();
        }

        private Task loadChunks()
        {
            return Task.Run(async () =>
            {
                if (!workLoad.CdmLoaded) return;
                Console.WriteLine($"Started Chunk Load WL#{workLoad.Id}");
                if (!workLoad.ChunksSetup)
                {
                    Console.WriteLine($"Started Chunk Setup WL#{workLoad.Id}");
                    ChunkBuilder.Create(script);
                    workLoad.ChunksSetup = true;
                    workLoad.Save();
                }
                var chunker = new ChunkBuilder(script);
                if (!workLoad.ChunksLoaded)
                {
                    Console.WriteLine($"Started Chunk Content Load WL#{workLoad.Id}");
                    chunker.Load(workLoad.ChunkSize);
                    var ordinals = analyzer.ChunkOrdinals();
                    ChunkTimer.Delete<ChunkTimer>(new { WorkLoadId = workLoad.Id });
                    foreach (var ordinal in ordinals)
                    {
                        (new ChunkTimer { WorkLoadId = (long)workLoad.Id, ChunkId = ordinal }).InsertOrUpdate();
                    }
                    workLoad.ChunksLoaded = true;
                    workLoad.Save();
                }
                List<ChunkTimer> chunks = NextChunks();
                Parallel.ForEach(chunks, new ParallelOptions { MaxDegreeOfParallelism = 3 }, (chunk) =>
                    {
                        RunChunk(chunk, chunker);
                    });
            });
        }

        private void RunChunk(ChunkTimer chunk, ChunkBuilder chunker)
        {
            Console.WriteLine($"{DateTime.Now}:: Started ChunkData#{chunk.Id} Load WL#{workLoad.Id}");
            chunk.StartTime = DateTime.Now;
            chunk.Touched = true;
            chunker.Run(chunk);
            chunk.EndTime = DateTime.Now;
            chunk.Save();
            Console.WriteLine($"{DateTime.Now}:: Completed ChunkData#{chunk.Id} Load WL#{workLoad.Id}");
        }

        private List<ChunkTimer> NextChunks() { return SysDB<ChunkTimer>.List(new { WorkLoadId = (long)workLoad.Id, Touched = false }); }

        private Task loadCdm()
        {
            return Task.Run(async () =>
            {
                if (workLoad.CdmLoaded) return;
                Console.WriteLine($"Started CDM Load WL#{workLoad.Id}");
                var loader = new LoadBuilder(script);
                Console.WriteLine($"Started CDM Load Table Create WL#{workLoad.Id}");
                loader.Create();
                Console.WriteLine($"Started CDM Load Table Runs WL#{workLoad.Id}");
                loader.Run();
                workLoad.CdmLoaded = true;
                workLoad.Save();
                Console.WriteLine($"Ended CDM Load Runs WL#{workLoad.Id}");
            });

        }

        private Task loadCleaner()
        {
            return Task.Run(() =>
            {
                (new CleanBuilder(script)).Run();
            });
        }

        private void LoadSchemas()
        {
            sourceSchema = SysDB<DBSchema>.Load(new { WorkLoadId = workLoad.Id, SchemaType = "source" });
            targetSchema = SysDB<DBSchema>.Load(new { WorkLoadId = workLoad.Id, SchemaType = "target" });
            vocabularySchema = SysDB<DBSchema>.Load(new { WorkLoadId = workLoad.Id, SchemaType = "vocabulary" });

            if (null == vocabularySchema || null == sourceSchema || null == targetSchema) throw new Exception("Ensure that all three schemas are set up and loaded!");
            script = new Script(sourceSchema, targetSchema, vocabularySchema);
            analyzer = new Analyzer(targetSchema);
        }
    }
}
