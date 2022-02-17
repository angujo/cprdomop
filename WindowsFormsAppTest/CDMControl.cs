using OMOPProcessor;
using System;
using System.Windows.Forms;
using SystemLocalStore.models;

namespace WindowsFormsAppTest
{
    public partial class CDMControl : UserControl
    {
        WorkLoad workLoad;
        public CDMControl()
        {
            InitializeComponent();
        }

        public CDMControl(WorkLoad wl) : this() { workLoad = wl; }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            if (null == workLoad)
            {
                MessageBox.Show(this, "WorkLoad is missing to push the Schedule to.", "No Workload");
                return;
            }
            (new CDMBuilder(workLoad)).Run();
        }
    }
}
