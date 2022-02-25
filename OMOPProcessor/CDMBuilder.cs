using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace OMOPProcessor
{
    public class CDMBuilder
    {
        WorkQueue workQueue;
        WorkLoad workLoad;
        DBSchema sourceSchema;
        DBSchema targetSchema;
        DBSchema vocabularySchema;
        Script script;
        Analyzer analyzer;
        CancellationTokenSource cancelToken = new CancellationTokenSource();

        public CDMBuilder(WorkQueue wq)
        {
            workQueue = wq;
            workLoad = SysDB<WorkLoad>.Load(new { Id = workQueue.WorkLoadId });
            LoadSchemas();
        }

        public Task RunAsync()
        {
            if (workLoad.CdmProcessed) return null;
           return Task.Run(() =>
               {
                   QueueTimer<WorkQueue>.Time(workQueue, workQueue.Id, () =>
                   {
                       try
                       {
                           ProcessAsync();
                           workQueue.Status = Status.COMPLETED;
                       }
                       catch (Exception ex)
                       {
                           workQueue.Status = Status.STOPPED;
                           Logger.Exception(ex);
                           //  throw;
                       }

                   },
                       new { Status = Status.STARTED });
               });
        }

        private async void ProcessAsync()
        {
            try
            {
                workLoad.IsRunning = true;
                workLoad.Save();
                Console.WriteLine($"Started Run for WL#{workLoad.Id}");
                await loadCdm();
                Console.WriteLine($"Prepare Chunk Load WL#{workLoad.Id}");
                if (workLoad.CdmLoaded && loadChunks())
                {
                    Console.WriteLine($"Start Cleaner WL#{workLoad.Id}");
                    await loadCleaner();
                    Console.WriteLine($"Ended Cleaner WL#{workLoad.Id}");
                    workLoad.CdmProcessed = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                // EventLog.WriteEntry(null, ex.Message, EventLogEntryType.Error);
                throw;
            }
            finally
            {
                workLoad.IsRunning = false;
                workLoad.Save();
            }
        }

        //<summary
        //@return Boolean Whether the chunks were fully loaded.
        // If TRUE, chunks are all done, otherwise FALSE
        //</summary>
        private bool loadChunks()
        {
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
            if (chunks.Count <= 0) return true;
            // var chunk = chunks.First();
            if (0 < workLoad.TestChunkCount) chunks = chunks.Take((int)workLoad.TestChunkCount).ToList();
            try
            {
                Parallel.ForEach(chunks, ParallelOptions(), (chunk) =>
                           {
                               RunChunk(chunk, chunker);
                           });
            }
            catch (Exception ex)
            {
                return false;
            }

            return 0 >= workLoad.TestChunkCount;
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

        private ParallelOptions ParallelOptions()
        {
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cancelToken.Token;
            parallelOptions.MaxDegreeOfParallelism = workLoad.MaxParallels;
            return parallelOptions;
        }
    }
}
