using OMOPProcessor;
using SystemLocalStore.models;

namespace OMOPService
{
    internal class ServiceWork
    {
        public static async void Process(WorkQueue workQueue)
        {
            switch (workQueue.QueueType)
            {
                case SystemLocalStore.QAction.SOURCE_FILE:
                    QueueRun.DoRun(workQueue);
                    break;
                case SystemLocalStore.QAction.OMOP_MAP:
                    Current.status = SystemLocalStore.Status.RUNNING;
                    var t = new CDMBuilder(workQueue).RunAsync();
                    if (null != t) await t;
                    Current.status = SystemLocalStore.Status.STOPPED;
                    break;
            }
        }
    }
}
