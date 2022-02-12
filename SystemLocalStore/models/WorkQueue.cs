using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLocalStore.models
{
    public class WorkQueue : AbsTable
    {
        public Int64 WorkLoadId { get; set; }
        public string Name { get; set; }
        public string QueueType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        public int? ProgressPercent { get; set; }
    }
}
