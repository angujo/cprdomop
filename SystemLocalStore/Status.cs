using Util;

namespace SystemLocalStore
{
    public enum Status
    {
        [StringValue("Started")]
        STARTED,
        [StringValue("Running")]
        RUNNING,
        [StringValue("Paused")]
        PAUSED,
        [StringValue("Stopped")]
        STOPPED,
        [StringValue("Error Encountered")]
        ERROR,
        [StringValue("Shutdown")]
        SHUTDOWN,
        [StringValue("On Queue")]
        QUEUED,
        [StringValue("Exit On Error")]
        ERROR_EXIT,
        [StringValue("Completed")]
        COMPLETED,
    }
}
