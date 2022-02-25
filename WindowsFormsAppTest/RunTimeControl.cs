using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SystemLocalStore.models;
using System.Timers;
using Util;
using System.ServiceProcess;
using SystemLocalStore;
using System.Threading.Tasks;
using OMOPProcessor;

namespace WindowsFormsAppTest
{
    public partial class RunTimeControl : UserControl
    {
        ServiceController sc;

        WorkLoad workLoad;
        public RunTimeControl()
        {
            InitializeComponent();
        }

        public RunTimeControl(WorkLoad load) : this()
        {
            workLoad = load;
            //  lblStatus.DataBindings.Add("Text", sCheck, "Status");
            ServiceCheck();
        }

        private void ServiceCheck()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 2000; // 4 seconds
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Start();
        }

        private void changeLabel(string stat)
        {
            // lblStatus.Text = stat;
            if (!IsHandleCreated) return;
            lblStatus.Invoke((Action)(() => lblStatus.Text = stat));
            lblTime.Invoke((Action)(() =>
            {
                var ss = SysDB<ServiceStatus>.Load();
                lblTime.Text = null == ss ? "[Service Time Fail]" : ss.LastRun.ToString();
            }));
            // Console.WriteLine(sc.DisplayName + " :: " + stat);
            // Console.WriteLine(stat);
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            // Logger.Info("Timer check");
            // Console.WriteLine($"[{DateTime.Now}] Timer Check");
            // return;
            if (null == sc)
            {
                sc = new ServiceController("OMOPService");
                changeLabel("Not Installed"); return;
            }
            sc.Refresh();
            switch (sc.Status)
            {
                case ServiceControllerStatus.Running:
                    changeLabel("Running"); break;
                case ServiceControllerStatus.Stopped:
                    changeLabel("Stopped"); break;
                case ServiceControllerStatus.Paused:
                    changeLabel("Paused"); break;
                case ServiceControllerStatus.StopPending:
                    changeLabel("Stopping"); break;
                case ServiceControllerStatus.StartPending:
                    changeLabel("Starting"); break;
                default:
                    changeLabel("Status Changing"); break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                new CDMBuilder(SysDB<WorkQueue>.Load(new { QueueType = QAction.OMOP_MAP })).RunAsync();
            });
        }
    }
}
