using System;
using System.Windows.Forms;
using SystemLocalStore.models;
using Util;

namespace WindowsFormsAppTest
{
    public partial class MainWindow : Form
    {
        UserControlForm userControlForm;
        public MainWindow()
        {
            InitializeComponent();
            Logger.InitEventLog();
            Logger.InitFileLog();
        }

        private void doLoad(WorkLoad workLoad)
        {
            if (null != userControlForm)
            {
                if (userControlForm.IsLoaded(workLoad)) return;
                userControlForm.Dispose();
            }
            if (workLoad.Id <= 0)
            {
                MessageBox.Show(this, "Invalid Work load loaded.\n Work Load need to be saved first before it can be loaded!", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.Text = workLoad.Name.Trim();
            userControlForm = new UserControlForm(workLoad);
            userControlForm.Parent = this;
            userControlForm.Dock = DockStyle.Fill;
            userControlForm.Show();
        }

        private void example2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var loads = WorkLoad.List<WorkLoad>();// DataAccess.loadWorkLoads();
            if (loads.Count <= 0)
            {
                MessageBox.Show(this, "There are no Workloads entered!", "Work Load Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var f = new WorkLoadList();
            foreach (var wl in loads)
            {
                ListViewItem lvRow = new ListViewItem(wl.Name, 0);
                lvRow.SubItems.Add(wl.ReleaseDate.ToString());
                lvRow.SubItems.Add(wl.Id.ToString());
                f.addItem(lvRow);
            }

            var res = f.ShowDialog();
            if (res == DialogResult.OK)
            {
                var ld = loads.Find(l => l.Id == f.selId);
                if (ld != null)
                {
                    doLoad(ld);
                }
                f.Dispose();
            }
            // doLoad("GOLD CDM 2020 07");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                Hide();
                ntTray.Visible = true;
            }
        }

        private void ntTray_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            Focus();
            ntTray.Visible = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show(this, "You are about to close this app while a process is running.\nDo you wish to proceed?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res != DialogResult.OK) e.Cancel = true;
        }

        private void mnNewWorkLoad_Click(object sender, EventArgs e)
        {
            var nw = new WorkLoad();
            if (DialogResult.Yes == (new WorkLoadForm(nw)).ShowDialog())
            {
                doLoad(nw);
            }
        }

        private void dataStorageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new SettingsForm()).ShowDialog();
        }
    }
}
