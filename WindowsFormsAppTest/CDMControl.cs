using OMOPProcessor;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace WindowsFormsAppTest
{
    public partial class CDMControl : UserControl
    {
        WorkLoad workLoad;
        public CDMControl()
        {
            InitializeComponent();
        }

        public CDMControl(WorkLoad wl) : this()
        {
            workLoad = wl;
            nudChunkSize.DataBindings.Add("Value", workLoad, "ChunkSize");
            nudTestChunk.DataBindings.Add("Value", workLoad, "TestChunkCount");
            nudParallels.DataBindings.Add("Value", workLoad, "MaxParallels");
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            if (null == workLoad)
            {
                MessageBox.Show(this, "WorkLoad is missing to push the Schedule to.", "No Workload");
                return;
            }
            try
            {
                Logger.EvtLog("Gettint started with EventLog Here!");
                // EventLog.WriteEntry("OmopCPRDSystem", "Gettint started with EventLog Here!", EventLogEntryType.Error);
                new CDMBuilder(SysDB<WorkLoad>.Load(new { Id = workLoad.Id })).RunAsync();
            }
            catch (Exception ex)
            {
                //eventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                // eventLog.WriteEntry(ex.StackTrace, System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show(null, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
                // throw;
            }
        }

        private void workLoadSave(object sender, EventArgs e)
        {
            if (0 < workLoad.Save()) MessageBox.Show(this, "Configuration saved successfully!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show(this, "Error Saving configuration!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
