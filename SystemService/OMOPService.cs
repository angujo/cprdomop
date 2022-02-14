using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore;
using SystemLocalStore.models;

namespace SystemService
{
    public partial class OMOPService : ServiceBase
    {
        private ServiceStatus serviceStatus;
        private static Status status;
        public OMOPService()
        {
            InitializeComponent();
            serviceStatus = ServiceStatus.LoadOrNew<ServiceStatus>();
            if (!serviceStatus.Exists())
            {
                serviceStatus.ServiceName = "OMOPService";
                serviceStatus.ServiceDescription = "Service that runs queues for the OMOP CDM Builder for CPRD Based Data(Oxford)";
                serviceStatus.InsertOrUpdate();
            }
        }

        protected override void OnStart(string[] args)
        {
            changeStatus(Status.RUNNING);
            QueueRun.DoRun();
        }

        protected override void OnStop()
        {
            Current.requestStatus = Status.STOPPED;
            changeStatus(Status.STOPPED);
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            changeStatus(Status.RUNNING);
        }

        protected override void OnPause()
        {
            base.OnPause();
            changeStatus(Status.PAUSED);
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
            changeStatus(Status.SHUTDOWN);
        }

        private void changeStatus(Status status) { serviceStatus.Status = status; serviceStatus.InsertOrUpdate(); }
    }
}
