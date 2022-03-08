using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace OMOPService
{
    public partial class MyNewService : ServiceBase
    {
        private SystemLocalStore.models.ServiceStatus sStatus;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
        public MyNewService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);


            // eventLog1.WriteEntry("In OnStart.");
            Logger.Info("Service started...");
            Logger.Info($"Work Directory: {Setting.InstallationDirectory}");
            Timer timer = new Timer();
            timer.Interval = 3000; // 3 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();


            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            Task.Run(() =>
            {
                if (Current.IsRunning()) return;
                WorkQueue workQueue = WorkQueue.NextAvailable();
                if (null == workQueue)
                {
                    DoCleanUp();
                    return;
                }
                ServiceWork.Process(workQueue);
            });

            if (null == sStatus)
            {
                sStatus = SysDB<SystemLocalStore.models.ServiceStatus>.LoadOrNew();
                if (!sStatus.Exists())
                {
                    sStatus.ServiceName = Setting.APP_NAME;
                    sStatus.ServiceDescription = "A service to run source file mapping and OMOP CDM transformation from source files.";
                }
            }
            sStatus.Status = Status.RUNNING;
            sStatus.LastRun = DateTime.Now;
            sStatus.Save();

            // Logger.Info($"Monitoring the system at {DateTime.Now}");
            //  eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        private void DoCleanUp()
        {
            if (SysDB<ChunkTimer>.Exists("Where Status = 8 AND EXISTS (SELECT 1 FROM CdmTimer c WHERE c.ChunkId = ChunkId AND c.WorkLoadId = WorkLoadId AND Status NOT IN (8))"))
            {
                Current.status = Status.RUNNING;
                Logger.Info("Query CheckUp Discrepancies found...");
                SysDB<AbsTable>.RunQuery("UPDATE WorkQueue SET Status = 8 FROM ChunkTimer t JOIN CdmTimer c ON c.ChunkId = t.ChunkId AND c.WorkLoadId = t.WorkLoadId AND c.Status <> 8 WHERE WorkQueue.WorkLoadId = t.WorkLoadId AND WorkQueue.QueueType = 1");
                SysDB<AbsTable>.RunQuery("UPDATE ChunkTimer SET Status = 6 FROM CdmTimer c WHERE c.ChunkId = ChunkTimer.ChunkId AND c.WorkLoadId = ChunkTimer.WorkLoadId AND c.Status <> 8");
                Current.status = Status.COMPLETED;
            }
        }

        protected override void OnStop()
        {
            Current.status = SystemLocalStore.Status.STOPPED;
            Logger.Info("...Service Stopped");
            //eventLog1.WriteEntry("In OnStop.");
        }
    }

    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public int dwServiceType;
        public ServiceState dwCurrentState;
        public int dwControlsAccepted;
        public int dwWin32ExitCode;
        public int dwServiceSpecificExitCode;
        public int dwCheckPoint;
        public int dwWaitHint;
    };
}
