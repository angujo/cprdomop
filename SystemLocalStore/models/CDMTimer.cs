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
        public Status Status { get; set; }
        public string ErrorLog { get; set; }

        public CDMTimer() { ChunkId = 0; }

        public static new string[] UpsColumns()
        {
            return new string[] { "Name", "ChunkId", "WorkLoadId" };
        }

        public static new string UpsIndex() { return "CDMTimer_Unique"; }
    }
}
