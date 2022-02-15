using FileProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemLocalStore;
using SystemLocalStore.models;
using SystemService;

namespace WindowsFormsAppTest
{
    public partial class SourceProcessControl : UserControl
    {
        WorkLoad workLoad;
        List<Queue> queues;

        public SourceProcessControl()
        {
            InitializeComponent();
        }

        public SourceProcessControl(WorkLoad load) : this()
        {
            workLoad = load;
        }

        public void SetWorkLoad(WorkLoad wl) { workLoad = wl; }

        private void btnLoad_Click(object sender, System.EventArgs e)
        {
            pbProgress.Visible = true;
            btnQueue.Enabled = false;
            btnLoad.Enabled = false;
            try
            {
                loadQueues(true);
                lvQueue.Items.Clear();
                foreach (var q in queues)
                {
                    ListViewItem listViewItem = new ListViewItem($"{q.TaskIndex}-{q.ParallelIndex}-{q.Ordinal}", 0);
                    listViewItem.SubItems.Add(q.FilePath);
                    listViewItem.SubItems.Add("0%");
                    lvQueue.Items.Add(listViewItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                btnQueue.Enabled = true;
                btnLoad.Enabled = true;
                pbProgress.Visible = false;
            }
        }

        private void loadQueues(bool force = false)
        {
            if (null != queues && !force) return;
            DBSchema source = DBSchema.Load<DBSchema>(new { WorkLoadId = workLoad.Id, SchemaType = "source" });
            DBSchema voc = DBSchema.Load<DBSchema>(new { WorkLoadId = workLoad.Id, SchemaType = "vocabulary" });
            if (null == source || null == voc) throw new Exception("Ensure both source and vocabulary schemas are set!");
            AbsDBMSProcessor processor = FileDBProcessor.GetProcessor(DataAccess.loadSourceFiles(workLoad).ToArray(), source, voc);
            queues = processor.GetQueue();
        }

        private async void btnQueue_Click(object sender, EventArgs e)
        {
            if (null == queues) { MessageBox.Show(null, "The Queue need to be loaded and reviewed before it can be pushed to scheduler!", "No Queue", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            pbProgress.Visible = true;
            btnQueue.Enabled = false;
            btnLoad.Enabled = false;
            try
            {
                await Task.Run(() =>
                {
                    var wq = WorkQueue.Load<WorkQueue>(new { WorkLoadId = workLoad.Id, QueueType = QAction.SOURCE_FILE }) ??
                    (new WorkQueue
                    {
                        WorkLoadId = (long)workLoad.Id,
                        Name = Guid.NewGuid().ToString(),
                        QueueType = QAction.SOURCE_FILE,
                        Status = Status.QUEUED
                    }).InsertOrUpdate(true);
                    Queue.Delete<Queue>(new { WorkQueueId = wq.Id });
                    Queue.InsertOrUpdate<Queue>(queues.Select(q => { q.WorkQueueId = (long)wq.Id; return q; }).ToList());
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                btnQueue.Enabled = true;
                btnLoad.Enabled = true;
                pbProgress.Visible = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QueueRun.DoRun();
        }
    }
}
