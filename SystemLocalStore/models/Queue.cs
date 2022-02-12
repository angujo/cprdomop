using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLocalStore.models
{
    public class Queue : AbsTable
    {
        public Int64 WorkQueueId { get; set; }
        public int? ParalellIndex { get; set; }
        public string FilePath { get; set; }
        public string FileContent { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        public QAction ActionType { get; set; }
    }
}
