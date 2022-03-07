using System;
using System.ServiceProcess;
using System.Timers;
using System.Windows.Forms;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace WindowsFormsAppTest
{
    public partial class UserControlForm : UserControl
    {
        private WorkLoad load;
        ServiceController sc;
        RunTimeControl runTimeControl;

        public UserControlForm()
        {
            InitializeComponent();
            ServiceCheck();
        }

        public UserControlForm(WorkLoad workl) : this()
        {
            load = workl;
            loadTitle.Text = workl.Name;
            if (load.CdmLoaded)
            {
                tabsHolder.TabPages.Remove(tabSource);
                tabsHolder.TabPages.Remove(tabConf);
            }
            else
            {
                addTabPage(pnConf, new ConfigControl(workl));
                addTabPage(pnSource, new SourceProcessControl(workl));
            }

            addTabPage(pnRuntime, runTimeControl = new RunTimeControl(workl));
            addTabPage(pnCdm, new CDMControl(workl));
            addTabPage(pnDB, new SchemaHolderControl(workl));
        }

        private void addTabPage(Panel pg, UserControl userControl)
        {
            pg.Controls.Clear();
            userControl.AutoScroll = true;
            //userControl.Dock = DockStyle.Fill;
            pg.Controls.Add(userControl);
        }

        public bool IsLoaded(WorkLoad work)
        {
            return work.Id.Equals(load.Id);
        }
        private void ServiceCheck()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 2000; // 4 seconds
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Start();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            // Logger.Info("Timer check");
            // Console.WriteLine($"[{DateTime.Now}] Timer Check");
            // return;
            try
            {
                if (null == sc)
                {
                    sc = new ServiceController(Setting.ServiceName);
                    lblName.Invoke((Action)(() => lblName.Text = Setting.ServiceName));
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
            catch (Exception ex)
            {
                sc = null;
                changeLabel("Not Setup");
            }

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

        private void tabsHolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  if(e.Tab)
        }

        private void tabsHolder_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabRuntime) runTimeControl.TabOpened();
            else runTimeControl.TabOpened(false);
        }
    }
}
