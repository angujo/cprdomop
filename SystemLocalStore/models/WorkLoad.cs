using System;

namespace SystemLocalStore.models
{
    public class WorkLoad : AbsTable
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool FilesLocked { get; set; }
        public bool SourceProcessed { get; set; }
        public bool CdmLoaded { get; set; }
        public bool ChunksSetup { get; set; }
        public bool ChunksLoaded { get; set; }
        public bool CdmProcessed { get; set; }
        public int ChunkSize { get; set; }
        public bool IsRunning { get; set; }
        public int MaxParallels { get; set; }
        public int TestChunkCount { get; set; }

        public WorkLoad()
        {
            ReleaseDate = DateTime.Now;
            ChunkSize = 500;
            MaxParallels = 3;
            TestChunkCount = 0;
        }
    }
}
