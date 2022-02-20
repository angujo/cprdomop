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
            QueueProcessor.SetCreator<CDMTimer>(key =>
            {
                // Console.WriteLine($"Preparing CDMTimer Load For: {key} WL#{workLoad.Id}");
                return new CDMTimer { WorkLoadId = (long)workLoad.Id, Name = key, ChunkId = 0, StartTime = DateTime.Now };
            });
            QueueProcessor.SetCreator<ChunkTimer>(key =>
            {
                // Console.WriteLine($"Preparing ChunkTimer Load For: {key} WL#{workLoad.Id}");
                return new ChunkTimer { WorkLoadId = (long)workLoad.Id, ChunkId = key, StartTime = DateTime.Now };
            });
        }

        public async Task RunAsync()
        {
            if (workLoad.CdmProcessed) return;
            Console.WriteLine($"Started Run for WL#{workLoad.Id}");
            await loadCdm();
            Console.WriteLine($"Prepare Chunk Load WL#{workLoad.Id}");
            await loadChunks();
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
                    await ChunkBuilder.Create(script);
                    workLoad.ChunksSetup = true;
                    workLoad.Save();
                }
                var chunker = new ChunkBuilder(script);
                if (!workLoad.ChunksLoaded)
                {
                    Console.WriteLine($"Started Chunk Content Load WL#{workLoad.Id}");
                    await chunker.Load(workLoad.ChunkSize);
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
                foreach (var chunk in chunks)
                {
                    Console.WriteLine($"{DateTime.Now}:: Started ChunkData#{chunk.Id} Load WL#{workLoad.Id}");
                    chunk.Touched = true;
                    chunk.Save();
                    await chunker.Run(chunk);
                    Console.WriteLine($"{DateTime.Now}:: Completed ChunkData#{chunk.Id} Load WL#{workLoad.Id}");
                }
            });

        }

        private List<ChunkTimer> NextChunks() { return ChunkTimer.List<ChunkTimer>(new { WorkLoadId = (long)workLoad.Id, Touched = false }); }

        private Task loadCdm()
        {
            return Task.Run(async () =>
            {
                if (workLoad.CdmLoaded) return;
                Console.WriteLine($"Started CDM Load WL#{workLoad.Id}");
                var loader = new LoadBuilder(script);
                Console.WriteLine($"Started CDM Load Table Create WL#{workLoad.Id}");
                await loader.Create();
                Console.WriteLine($"Started CDM Load Table Runs WL#{workLoad.Id}");
                await loader.Run();
                workLoad.CdmLoaded = true;
                workLoad.Save();
                Console.WriteLine($"Ended CDM Load Runs WL#{workLoad.Id}");
            });

        }

        private void LoadSchemas()
        {
            sourceSchema = DBSchema.Load<DBSchema>(new { WorkLoadId = workLoad.Id, SchemaType = "source" });
            targetSchema = DBSchema.Load<DBSchema>(new { WorkLoadId = workLoad.Id, SchemaType = "target" });
            vocabularySchema = DBSchema.Load<DBSchema>(new { WorkLoadId = workLoad.Id, SchemaType = "vocabulary" });

            if (null == vocabularySchema || null == sourceSchema || null == targetSchema) throw new Exception("Ensure that all three schemas are set up and loaded!");
            script = new Script(sourceSchema, targetSchema, vocabularySchema);
            analyzer = new Analyzer(targetSchema);
        }
    }
}
