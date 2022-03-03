using System;

namespace SystemLocalStore.models
{
    public class ChunkTimer : AbsUpsTable
    {
        public int ChunkId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Touched { get; set; }
        public Int64 WorkLoadId { get; set; }
        public Status Status { get; set; }
        public string ErrorLog { get; set; }

        public static string[] UpsColumns()
        {
            return new string[] { "ChunkId", "WorkLoadId" };
        }
        public static new string UpsIndex() { return "ChunkTimer_Unique"; }
    }
}
