using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore;
using SystemLocalStore.models;

namespace SystemService
{
    internal static class Current
    {
        public static Status requestStatus { get; set; }
        public static Status status { get; set; }
        public static WorkQueue workQueue { get; set; }

        public static bool IsRunning() { return Status.RUNNING == status; }
        public static bool IsAvailable() { return Status.RUNNING != status; }

        public static bool StopRequested() { return requestStatus == Status.STOPPED; }
    }
}
