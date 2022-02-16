using System;

namespace SystemLocalStore.models
{
    public class ServiceStatus : AbsTable
    {
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public Status Status { get; set; }
        public DateTime LastRun { get; set; }

        public ServiceStatus() { }
    }
}
