using System;

namespace SystemLocalStore.models
{
    public class WorkQueue : AbsUpsTable
    {
        public Int64 WorkLoadId { get; set; }
        public string Name { get; set; }
        public QAction QueueType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        public int? ProgressPercent { get; set; }

        public static new string[] UpsColumns() { return new string[] { "WorkLoadId", "QueueType" }; }

        public static WorkQueue NextAvailable() { return ByStatusAvailable(); }

        private static WorkQueue ByStatusAvailable()
        {
            var stats = new Status[] { Status.PAUSED, Status.QUEUED, Status.STOPPED }; //The order in which queues to be accessed
            foreach (var stat in stats)
            {
                var sts = DataAccess.ForQueues(stat);
                if (sts != null) return sts;
            }
            return null;
        }
    }
}
