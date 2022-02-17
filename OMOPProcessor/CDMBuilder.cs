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
            loadCdm();
        }

        private void loadChunks()
        {
            var chunker = new ChunkBuilder(script);
            if (!workLoad.ChunksSetup)
            {
                chunker.Load();
            }
        }

        private async void loadCdm()
        {
            if (workLoad.CdmLoaded) return;
            var loader = new LoadBuilder(script);
            await loader.Create();
            await loader.Run();
            workLoad.CdmLoaded = true;
            workLoad.InsertOrUpdate();
        }

        private void LoadSchemas()
        {
            sourceSchema = DBSchema.Load<DBSchema>(new { WorkLoadId = workLoad.Id, SchemaType = "source" });
            targetSchema = DBSchema.Load<DBSchema>(new { WorkLoadId = workLoad.Id, SchemaType = "target" });
            vocabularySchema = DBSchema.Load<DBSchema>(new { WorkLoadId = workLoad.Id, SchemaType = "vocabulary" });

            if (null == vocabularySchema || null == sourceSchema || null == targetSchema) throw new Exception("Ensure that all three schemas are set up and loaded!");
            script = new Script(sourceSchema, targetSchema, vocabularySchema);
        }
    }
}
