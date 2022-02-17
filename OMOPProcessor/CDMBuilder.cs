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
                return CDMTimer.LoadOrNew<CDMTimer>(new { WorkLoadId = workLoad.Id, Name = key });
            });
            QueueProcessor.SetCreator<ChunkTimer>(key =>
            {
                return ChunkTimer.LoadOrNew<ChunkTimer>(new { WorkLoadId = workLoad.Id, ChunkId = key });
            });
        }

        public void Run()
        {
            if (workLoad.CdmProcessed) return;
            loadCdm();
            loadChunks();
        }

        private async void loadChunks()
        {
            if (!workLoad.ChunksSetup)
            {
                await ChunkBuilder.Create(script);
                workLoad.ChunksSetup = true;
                workLoad.Save();
            }
            var chunker = new ChunkBuilder(script);
            if (!workLoad.ChunksLoaded)
            {
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
            var chunk = ChunkTimer.Load<ChunkTimer>(new { WorkLoadId = (long)workLoad.Id, Touched = false });
            if (null == chunk) return;
            //TODO Loop here till no chunk -LATER
             await chunker.Run(chunk);
        }

        private async void loadCdm()
        {
            if (workLoad.CdmLoaded) return;
            var loader = new LoadBuilder(script);
            await loader.Create();
            await loader.Run();
            workLoad.CdmLoaded = true;
            workLoad.Save();
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
