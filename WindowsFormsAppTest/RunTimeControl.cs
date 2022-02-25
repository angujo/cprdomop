using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SystemLocalStore.models;
using System.Timers;
using Util;
using System.ServiceProcess;

namespace WindowsFormsAppTest
{
    public partial class RunTimeControl : UserControl
    {
        const string SERVICE_NAME = "SystemService.exe";
        ServiceController sc;
        ServiceCheck sCheck = new ServiceCheck();

        string path;
        WorkLoad workLoad;
        public RunTimeControl()
        {
            InitializeComponent();
            path = Path.Combine(Environment.CurrentDirectory, SERVICE_NAME);
        }

        public RunTimeControl(WorkLoad load) : this()
        {
            workLoad = load;
            //  lblStatus.DataBindings.Add("Text", sCheck, "Status");
            ServiceCheck();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (!checkInstaller()) return;
            Console.WriteLine(path);
            ExecuteCommand($"{path} -install");
        }
        private void ExecuteCommand(string Command)
        {
            ProcessStartInfo ProcessInfo;
            Process Process;
            string cmd = $"";

            ProcessInfo = new ProcessStartInfo("cmd.exe", "/K " + Command);
            ProcessInfo.CreateNoWindow = false;
            ProcessInfo.UseShellExecute = true;

            Process = Process.Start(ProcessInfo);
        }

        private bool checkInstaller()
        {
            if (!File.Exists(path))
            {
                MessageBox.Show(null, $"Unable to locate the service installer '{path}'.", MessageBoxButtons.OK, MessageBoxIcon.Error); return false;
            }
            return true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!checkInstaller()) return;
            Console.WriteLine(path);
            ExecuteCommand($"{path} -uninstall");
        }

        private void ServiceCheck()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 2000; // 60 seconds
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Start();
        }

        private void changeLabel(string stat)
        {
            // lblStatus.Text = stat;
            lblStatus.Invoke((Action)(() => lblStatus.Text = stat));
            Console.WriteLine(sc.DisplayName + " :: " + stat);
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
    }
    internal class ServiceCheck : ReactiveProperties
    {
        public string Status { get { return getProperty<string>("Status"); } set { setProperty("Status", value); } }
    }
}
