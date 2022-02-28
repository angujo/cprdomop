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
        TimerLogger cdmLogger;

        public CDMBuilder(WorkQueue wq)
        {
            workQueue = wq;
            workLoad = SysDB<WorkLoad>.Load(new { Id = workQueue.WorkLoadId });
            cdmLogger = new TimerLogger(workLoad);
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
                            workQueue.Status = Status.STARTED;
                            Logger.Info($"WorkQueue<QueueTime>#{workQueue.Id} Starter: CDMBuilder");
                            ProcessAsync();
                            workQueue.Status = Status.COMPLETED;
                            Logger.Info($"WorkQueue<QueueTime>#{workQueue.Id} Ender: CDMBuilder");
                        }
                        catch (Exception ex)
                        {
                            Logger.Info($"WorkQueue<QueueTime>#{workQueue.Id} Exception: CDMBuilder");
                            workQueue.Status = Status.STOPPED;
                            Logger.Exception(ex);
                            //  throw;
                        }
                    });
                });
        }

        private void ProcessAsync()
        {
            try
            {
                workLoad.IsRunning = true;
                workLoad.Save();
                Logger.Info($"Started Run for WL#{workLoad.Id}");
                loadCdm();
                Logger.Info($"Prepare Chunk Load WL#{workLoad.Id}");
                if (workLoad.CdmLoaded && loadChunks())
                {
                    Logger.Info($"Start Cleaner WL#{workLoad.Id}");
                    loadCleaner();
                    Logger.Info($"Ended Cleaner WL#{workLoad.Id}");
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
            Logger.Info($"Started Chunk Load WL#{workLoad.Id}");
            if (!workLoad.ChunksSetup)
            {
                Logger.Info($"Started Chunk Setup WL#{workLoad.Id}");
                ChunkBuilder.Create(script);
                workLoad.ChunksSetup = true;
                workLoad.Save();
            }
            var chunker = new ChunkBuilder(script);
            if (!workLoad.ChunksLoaded)
            {
                Logger.Info($"Started Chunk Content Load WL#{workLoad.Id}");
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
            Logger.Info($"Total Chunks: {chunks.Count}");
            List<ChunkTimer> chunkQueues = (0 < workLoad.TestChunkCount) ? chunks.Take((int)workLoad.TestChunkCount).ToList() : chunks;
            Logger.Info($"Queued Chunks: {chunkQueues.Count} of {workLoad.TestChunkCount}");
            try
            {
                Parallel.ForEach(chunkQueues, ParallelOptions(), (chunk) =>
                           {
                               RunChunk(chunk, chunker);
                           });
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }

            return 0 >= workLoad.TestChunkCount;
        }

        private void RunChunk(ChunkTimer chunk, ChunkBuilder chunker)
        {
            Logger.Info($"Started Preparing CDM Timer Logger ChunkID#{chunk.Id} Load WL#{workLoad.Id}");
            cdmLogger.prepareCDMTimers(chunk);
            Logger.Info($"Ended Preparation of CDM Timer Logger ChunkID#{chunk.Id} Load WL#{workLoad.Id}");
            Logger.Info($"Started ChunkData#{chunk.Id} Load WL#{workLoad.Id}");
            QueueTimer<ChunkTimer>.Time(chunk, chunk.Id, () =>
            {
                chunker.Run(chunk);
            }, new { Touched = true });
            /* chunk.StartTime = DateTime.Now;
             chunk.Touched = true;
             chunker.Run(chunk);
             chunk.EndTime = DateTime.Now;
             chunk.Save();*/
            Logger.Info($"Completed ChunkData#{chunk.Id} Load WL#{workLoad.Id}");
        }

        private List<ChunkTimer> NextChunks() { return SysDB<ChunkTimer>.List(new { WorkLoadId = (long)workLoad.Id, Touched = false }); }

        private void loadCdm()
        {
            if (workLoad.CdmLoaded) return;
            Logger.Info($"Started CDM Load WL#{workLoad.Id}");
            var loader = new LoadBuilder(script);
            Logger.Info($"Started CDM Load Table Create WL#{workLoad.Id}");
            loader.Create();
            Logger.Info($"Started CDM Load Table Runs WL#{workLoad.Id}");
            loader.Run();
            workLoad.CdmLoaded = true;
            workLoad.Save();
            Logger.Info($"Ended CDM Load Runs WL#{workLoad.Id}");

        }

        private void loadCleaner()
        {
            (new CleanBuilder(script)).Run();
        }

        private void LoadSchemas()
        {
            sourceSchema = SysDB<DBSchema>.Load(new { WorkLoadId = workLoad.Id, SchemaType = "source" });
            targetSchema = SysDB<DBSchema>.Load(new { WorkLoadId = workLoad.Id, SchemaType = "target" });
            vocabularySchema = SysDB<DBSchema>.Load(new { WorkLoadId = workLoad.Id, SchemaType = "vocabulary" });

            if (null == vocabularySchema || null == sourceSchema || null == targetSchema) throw new Exception("Ensure that all three schemas are set up and loaded!");
            script = new Script(sourceSchema, targetSchema, vocabularySchema, cdmLogger);
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
