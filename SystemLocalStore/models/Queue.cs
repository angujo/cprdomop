using System;

namespace SystemLocalStore.models
{
    public class Queue : AbsTable
    {
        public Int64 WorkQueueId { get; set; }
        public Int64 DBSchemaId { get; set; }
        public int? TaskIndex { get; set; }
        public int? ParallelIndex { get; set; }
        public string FilePath { get; set; }
        public string FileContent { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        public QAction ActionType { get; set; }
        public int Ordinal { get; set; }
        public string ErrorLog { get; set; }
    }
}
