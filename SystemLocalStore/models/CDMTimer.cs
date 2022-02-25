using System;

namespace SystemLocalStore.models
{
    public class CDMTimer : AbsUpsTable
    {
        public string Name { get; set; }
        public int ChunkId { get; set; }
        public string Query { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Int64 WorkLoadId { get; set; }

        public CDMTimer() { ChunkId = 0; }

        public static string[] UpsColumns()
        {
            return new string[] { "Name", "ChunkId", "WorkLoadId" };
        }
    }
}
