using System;
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
            if (wl.ChunksSetup) nudChunkSize.Enabled = false;
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            // workLoad = SysDB<WorkLoad>.Load(new { Id = workLoad.Id });
            if (null == workLoad)
            {
                MessageBox.Show(this, "WorkLoad is missing to push the Schedule to.", "No Workload");
                return;
            }
            if (workLoad.CdmProcessed &&
                DialogResult.Cancel == MessageBox.Show(this, "WorkLoad has already been mapped to completion.\n Do you wish to overwrite and re-map?", "No Workload", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                return;
            }
            try
            {
                Logger.EvtLog("Gettint started with EventLog Here!");
                // EventLog.WriteEntry("OmopCPRDSystem", "Gettint started with EventLog Here!", EventLogEntryType.Error);
                //   new CDMBuilder(SysDB<WorkLoad>.Load(new { Id = workLoad.Id })).RunAsync();

                if (SysDB<WorkQueue>.Exists("Where WorkLoadId = @WorkLoadId AND QueueType= @QueueType AND Status IN (@Status, @QStatus)", new { WorkLoadId = workLoad.Id, QueueType = QAction.OMOP_MAP, Status = (int)Status.STARTED, QStatus = (int)Status.QUEUED }))
                {
                    MessageBox.Show(null, "Queue is already running or scheduled.\nCheck on the service status or Error Log!", "Scheduled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                WorkQueue wq = SysDB<WorkQueue>.LoadOrNew("Where WorkLoadId = @WorkLoadId AND QueueType= @QueueType", new { WorkLoadId = (long)workLoad.Id, QueueType = QAction.OMOP_MAP, });
                wq.Status = Status.QUEUED;
                wq.Name = Guid.NewGuid().ToString();
                wq.Save();
                MessageBox.Show(null, "Queue Added successfully and will be run on next schedule.\nEnsure the Service is running!", "Scheduled", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                //eventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                // eventLog.WriteEntry(ex.StackTrace, System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show(null, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(ex.Message);
                Logger.Error(ex.ToString());
                // throw;
            }
        }

        private void workLoadSave(object sender, EventArgs e)
        {
            Console.WriteLine($"Worklod #{workLoad.Id}");
            if (0 < workLoad.Save()) MessageBox.Show(this, "Configuration saved successfully!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show(this, "Error Saving configuration!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
