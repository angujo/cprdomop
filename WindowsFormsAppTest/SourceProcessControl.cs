using FileProcessor;
using System;
using System.Collections.Generic;
using System.IO;
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

        private async void btnLoad_ClickAsync(object sender, System.EventArgs e)
        {
            await UIActionAsync(() => { loadQueues(true); });
            lvQueue.Items.Clear();
            foreach (var q in queues)
            {
                ListViewItem listViewItem = new ListViewItem($"{q.TaskIndex}-{q.ParallelIndex}-{q.Ordinal}", 0);
                listViewItem.SubItems.Add(q.FilePath);
                listViewItem.SubItems.Add("0%");
                lvQueue.Items.Add(listViewItem);
            }
        }

        private void loadQueues(bool force = false)
        {
            if (null != queues && !force) return;
            DBSchema source = SysDB<DBSchema>.Load(new { WorkLoadId = workLoad.Id, SchemaType = "source" });
            DBSchema voc = SysDB<DBSchema>.Load(new { WorkLoadId = workLoad.Id, SchemaType = "vocabulary" });
            if (null == source || null == voc) throw new Exception("Ensure both source and vocabulary schemas are set!");
            AbsDBMSProcessor processor = FileDBProcessor.GetProcessor(DataAccess.loadSourceFiles(workLoad).ToArray(), source, voc);
            queues = processor.GetQueue();
        }

        private async void btnQueue_Click(object sender, EventArgs e)
        {
            if (null == queues) { MessageBox.Show(null, "The Queue need to be loaded and reviewed before it can be pushed to scheduler!", "No Queue", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            await UIActionAsync(() =>
               {
                   var wq = SysDB<WorkQueue>.Load(new { WorkLoadId = workLoad.Id, QueueType = QAction.SOURCE_FILE }) ??
                          (new WorkQueue
                          {
                              WorkLoadId = (long)workLoad.Id,
                              Name = Guid.NewGuid().ToString(),
                              QueueType = QAction.SOURCE_FILE,
                              Status = Status.QUEUED
                          }).InsertOrUpdate(true);
                   SysDB<Queue>.Delete(new { WorkQueueId = wq.Id });
                   SysDB<Queue>.InsertOrUpdate(queues.Select(q => { q.WorkQueueId = (long)wq.Id; return q; }).ToList());
               });
        }

        private async Task UIActionAsync(Action act)
        {
            pbProgress.Visible = true;
            btnQueue.Enabled = false;
            btnLoad.Enabled = false;
            button1.Enabled = false;
            try
            {
                await Task.Run(() =>
                {
                    act();
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
                button1.Enabled = true;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //  Console.WriteLine("Testing output to console....");
            await UIActionAsync(() => { QueueRun.DoRun(); });
            // CleanLineFeeds(@"D:\temp\cdm\vocabulary\CONCEPT_RELATIONSHIP.csv");
        }


    }
}
