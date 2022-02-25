using System;
using System.ServiceProcess;
using System.Timers;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace SystemService
{
    public partial class OMOPService : ServiceBase
    {
        private ServiceStatus serviceStatus;
        private static Status status;
        public OMOPService()
        {
            InitializeComponent();
            Logger.Info("Service Initialized!");
            /*serviceStatus = SysDB<ServiceStatus>.LoadOrNew();
            if (!serviceStatus.Exists())
            {
                serviceStatus.ServiceName = "OMOPService";
                serviceStatus.ServiceDescription = "Service that runs queues for the OMOP CDM Builder for CPRD Based Data(Oxford)";
                serviceStatus.Save();
            }*/
        }

        protected override void OnStart(string[] args)
        {
            Logger.Info("Service Started!");
            // changeStatus(Status.RUNNING);
            // Timer timer = new Timer();
            // timer.Interval = 60000; // 60 seconds
            // timer.Elapsed += new ElapsedEventHandler(OnTimer);
            // timer.Start();
            //  QueueRun.DoRun();
        }

        protected override void OnStop()
        {
            // Current.requestStatus = Status.STOPPED;
            // changeStatus(Status.STOPPED);
            Logger.Info("Service Stopped!");
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            // changeStatus(Status.RUNNING);
            Logger.Info("Service Continued!");
            // QueueRun.DoRun();
        }

        protected override void OnPause()
        {
            // Current.requestStatus = Status.STOPPED;
            // changeStatus(Status.PAUSED);
            Logger.Info("Service Paused!");
            base.OnPause();
        }

        protected override void OnShutdown()
        {
            // Current.requestStatus = Status.STOPPED;
            //  changeStatus(Status.SHUTDOWN);
            Logger.Info("Service Shutdown!");
            base.OnShutdown();
        }

        private void changeStatus(Status _status) { status = serviceStatus.Status = _status; serviceStatus.Save(); }

        private void OnTimer(object sender, ElapsedEventArgs args)
        {
            Logger.Info("AM a service runner!");
        }

        public void Process()
        {
            Logger.Info("AM a service runner!");
            // Console.WriteLine(Environment.CurrentDirectory);
            // Console.WriteLine(Setting.LogFilePath);
            Console.WriteLine("Am written!");
        }
    }
}
