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
            try
            {
                workQueue = wq;
                workLoad = SysDB<WorkLoad>.Load("Where Id = @Id", new { Id = workQueue.WorkLoadId });
                cdmLogger = new TimerLogger(workLoad);
                LoadSchemas();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Console.WriteLine(ex.ToString());
                throw;
            }
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
                            throw;
                        }
                    });
                });
        }

        private void ProcessAsync()
        {
            try
            {
                int loads = 10;
                workLoad.IsRunning = true;
                workLoad.Save();
                Logger.Info($"Started Run for WL#{workLoad.Id}");
                loadCdm();
                Logger.Info($"Prepare Chunk Load WL#{workLoad.Id}");
            LoadAgain:
                loadChunks();
                var ex = SysDB<ChunkTimer>.Exists("Where WorkLoadId = @WorkLoadId AND Status <> @Status", new { Status = Status.COMPLETED, WorkLoadId = workLoad.Id });
                if (0 == workLoad.TestChunkCount && ex)
                {
                    loads--;
                    if (0 == loads) { workQueue.Status = Status.ERROR_EXIT; Logger.Error("Exceeded number of ProcessAsync Loads!"); }
                    else goto LoadAgain;
                }
                if (workLoad.CdmLoaded && !ex)
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
        private void loadChunks()
        {
            Logger.Info($"Started Chunk Load WL#{workLoad.Id}");
            if (!workLoad.ChunksSetup)
            {
                Logger.Info($"Started Chunk Setup WL#{workLoad.Id}");
                ChunkBuilder.Create(script);
                workLoad.ChunksSetup = true;
                SysDB<ChunkTimer>.Delete(new { WorkLoadId = workLoad.Id });
                SysDB<CDMTimer>.Delete("Where WorkLoadId = @WorkLoadId AND ChunkId <> -1", new { WorkLoadId = workLoad.Id });
                workLoad.ChunksLoaded = false;
                workLoad.Save();
            }
            var chunker = new ChunkBuilder(script);
            if (!workLoad.ChunksLoaded)
            {
                Logger.Info($"Started Chunk Content Load WL#{workLoad.Id}");
                chunker.Load(workLoad.ChunkSize);
                var ordinals = analyzer.ChunkOrdinals();
                Logger.Info($"Started Cleanup of ChunkTimer Content Load WL#{workLoad.Id}");
                ChunkTimer.Delete<ChunkTimer>(new { WorkLoadId = workLoad.Id });
                Logger.Info($"DONE Cleanup of ChunkTimer Content Load WL#{workLoad.Id}");
                Logger.Info($"Started Populating of ChunkTimer WL#{workLoad.Id}");
                foreach (var ordinal in ordinals)
                {
                    (new ChunkTimer { WorkLoadId = (long)workLoad.Id, ChunkId = ordinal }).InsertOrUpdate();
                }
                workLoad.ChunksLoaded = true;
                workLoad.Save();
                Logger.Info($"Done Populating of ChunkTimer WL#{workLoad.Id}");
            }
            List<int> chunks = NextChunks();
            // foreach (var chunk in chunks) Console.WriteLine(chunk); return false;
            // var chunk = chunks.First();
            Logger.Info($"Called Chunks: {chunks.Count}");
            // List<ChunkTimer> chunkQueues = (0 < workLoad.TestChunkCount) ? chunks.Take((int)workLoad.TestChunkCount).ToList() : chunks;
            // Logger.Info($"Queued Chunks: {chunkQueues.Count} of {workLoad.TestChunkCount}");
            try
            {
                Parallel.ForEach(chunks, ParallelOptions(), (chunkId) =>
                {
                    var chunk = SysDB<ChunkTimer>.Load("Where ChunkId = @ChunkId AND WorkLoadId = @WorkLoadId", new { ChunkId = chunkId, WorkLoadId = workLoad.Id });
                    try
                    {
                        RunChunk(chunk, chunker);
                    }
                    catch (Exception ex)
                    {
                        Logger.Exception(ex);
                        chunk.ErrorLog = ex.Message;
                        chunk.Save();
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void RunChunk(ChunkTimer chunk, ChunkBuilder chunker)
        {
            if (null == chunk || !chunk.Exists()) return;
            Logger.Info($"Started ChunkData#{chunk.ChunkId} Load WL#{workLoad.Id}");
            try
            {
                chunk.Status = Status.STARTED;
                chunk.StartTime = DateTime.Now;
                chunk.ErrorLog = null;
                chunk.Touched = true;
                chunk.Save();

                chunker.Run(chunk);
                chunk.Status = Status.COMPLETED;
            }
            catch (Exception ex)
            {
                chunk.Status = Status.ERROR_EXIT;
                chunk.ErrorLog = ex.Message;
                Logger.Exception(ex);
            }
            finally
            {
                chunk.EndTime = DateTime.Now;
                chunk.Touched = false;
                chunk.Save();
            }

            /* chunk.StartTime = DateTime.Now;
             chunk.Touched = true;
             chunker.Run(chunk);
             chunk.EndTime = DateTime.Now;
             chunk.Save();*/
            Logger.Info($"Completed ChunkData#{chunk.ChunkId} Load WL#{workLoad.Id}");
        }

        private List<int> NextChunks()
        {
            return SysDB<ChunkTimer>.Column<int>("ChunkId", "Where WorkLoadId = @WorkLoadId AND Status <> @Status ORDER BY ChunkId ASC " + (0 < workLoad.TestChunkCount ? $"LIMIT {workLoad.TestChunkCount}" : string.Empty),
                new { WorkLoadId = (long)workLoad.Id, Touched = false, Status = Status.COMPLETED });
        }

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
            sourceSchema = SysDB<DBSchema>.Load("Where WorkLoadId= @WorkLoadId AND  SchemaType= @SchemaType ", new { WorkLoadId = workLoad.Id, SchemaType = "source" });
            targetSchema = SysDB<DBSchema>.Load("Where WorkLoadId= @WorkLoadId AND  SchemaType= @SchemaType ", new { WorkLoadId = workLoad.Id, SchemaType = "target" });
            vocabularySchema = SysDB<DBSchema>.Load("Where WorkLoadId= @WorkLoadId AND SchemaType = @SchemaType ", new { WorkLoadId = workLoad.Id, SchemaType = "vocabulary" });

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
