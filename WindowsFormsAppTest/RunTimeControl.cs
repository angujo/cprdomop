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
using System.Drawing;

namespace WindowsFormsAppTest
{
    public partial class RunTimeControl : UserControl
    {
        bool tabOpen = false;
        bool queryLoaded = false;
        bool chunkLoaded = false;
        bool chunkRun = false;
        bool queryRun = false;
        WorkLoad workLoad;
        int? selectedOrdinal;
        bool chunkView = false;
        System.Timers.Timer timer = new System.Timers.Timer();
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
            timer.Interval = 4000; // 4 seconds
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            loadQueries();
            loadChunks();
        }

        private void loadChunkTimer(Action<ChunkTimer> setView)
        {
            var chunks = SysDB<ChunkTimer>.List("Where WorkLoadId= @WorkLoadId", new { WorkLoadId = workLoad.Id });
            foreach (var chunk in chunks)
            {
                setView(chunk);
            }
        }

        private void loadQueryTimer(Action<CDMTimer> setView)
        {
            var timers = SysDB<CDMTimer>.List($"Where WorkLoadId = @WorkLoadId { (null != selectedOrdinal ? $"AND ChunkId = {(int)selectedOrdinal}" : string.Empty)}", new { WorkLoadId = workLoad.Id, });
            foreach (var timer in timers)
            {
                setView(timer);
            }
        }

        private ProgressBar GetProgressBar(long key, Status status)
        {
            ProgressBar progressBar = new ProgressBar();
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Name = $"{key}";
            progressBar.Value = new Random().Next(100);
            return progressBar;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                new CDMBuilder(SysDB<WorkQueue>.Load("Where QueueType = @QueueType", new { QueueType = QAction.OMOP_MAP })).RunAsync();
            });
        }

        public void TabOpened(bool isOpen = true)
        {
            if (isOpen)
            {
                timer.Start();
                loadQueries();
                loadChunks();
            }
            else
            {
                timer.Stop();
            }

        }

        private void loadQueries()
        {
            Task.Run(() =>
           {
               if (!queryRun)
               {
                   //lvQuery.Invoke(new Action(() => lvQuery.Items.Clear()));
                   queryRun = true;
                   pbQuery.Invoke(new Action(() => { pbQuery.Visible = true; }));
                   loadQueryTimer((timer) =>
                   {
                       lvQuery.Invoke(new Action(() =>
                       {
                           if (queryLoaded) UpdateLVItemValue(timer, lvQuery);
                           else lvQuery.Items.Add(QueryLVItem(timer));
                       }));
                   });
                   pbQuery.Invoke(new Action(() => { pbQuery.Visible = false; }));
                   queryRun = false;
                   queryLoaded = true;
               }
           });
        }

        private void loadChunks()
        {
            Task.Run(() =>
                       {
                           if (!chunkRun)
                           {
                               //lvChunks.Invoke(new Action(() => lvChunks.Items.Clear()));
                               chunkRun = true;
                               pbChunks.Invoke(new Action(() => { pbChunks.Visible = true; }));
                               loadChunkTimer((chunk) =>
                               {
                                   lvChunks.Invoke(new Action(() =>
                                   {
                                       if (chunkLoaded) UpdateLVItemValue(chunk, lvChunks);
                                       else lvChunks.Items.Add(ChunkLVItem(chunk));
                                   }));
                               });
                               pbChunks.Invoke(new Action(() => { pbChunks.Visible = false; }));
                               chunkRun = false;
                               chunkLoaded = true;
                           }
                       });
        }

        private ListViewItem QueryLVItem(CDMTimer timer)
        {
            var n = (chunkView ? string.Empty : $"(#{timer.ChunkId}) ") + timer.Name;
            ListViewItem item = new ListViewItem();
            var starts = Status.STARTED == timer.Status && timer.StartTime.HasValue ? timer.StartTime.Value.ToString("yyyy-MM-dd H:mm:ss") : String.Empty;
            var ends = Status.COMPLETED == timer.Status && timer.EndTime.HasValue ? timer.EndTime.Value.ToString("yyyy-MM-dd H:mm:ss") : String.Empty;
            item.SubItems[0].Text = n;
            item.SubItems.Add(starts);
            item.SubItems.Add(ends);
            item.SubItems.Add(timer.Status.GetStringValue());
            item.Name = timer.Id.ToString();
            item.SubItems.Add($"{timer.Id}");
            item.Tag = timer.Id;
            return item;
        }

        private ListViewItem ChunkLVItem(ChunkTimer chunk)
        {
            ListViewItem item = new ListViewItem();
            var starts = chunk.Touched && chunk.StartTime.HasValue ? chunk.StartTime.Value.ToString("yyyy-MM-dd H:mm:ss") : String.Empty;
            var ends = Status.COMPLETED == chunk.Status && chunk.EndTime.HasValue ? chunk.EndTime.Value.ToString("yyyy-MM-dd H:mm:ss") : String.Empty;
            item.SubItems[0].Text = $"{chunk.ChunkId}";
            item.SubItems.Add(starts);
            item.SubItems.Add(ends);
            item.SubItems.Add(chunk.Status.GetStringValue());
            item.SubItems.Add($"{chunk.Id}");
            item.SubItems.Add($"{chunk.ChunkId}");
            item.Tag = chunk.Id;
            // item.Name = chunk.Id.ToString();
            return item;
        }

        private void UpdateLVItemValue(AbsTable loader, ListView l_view)
        {
            ListViewItem lvi;
            if (null == (lvi = fromCollection(l_view, (long)loader.Id))) return;
            // find the LVI based on the "key" in 
            //var index = 4;// typeof(ChunkTimer) == loader.GetType()?4:4;
            if (typeof(ChunkTimer) == loader.GetType())
            {
                var _loader = (ChunkTimer)loader;
                if (null == lvi) return;
                var starts = _loader.Touched && _loader.StartTime.HasValue ? _loader.StartTime.Value.ToString("yyyy-MM-dd H:mm:ss") : String.Empty;
                var ends = Status.COMPLETED == _loader.Status && _loader.EndTime.HasValue ? _loader.EndTime.Value.ToString("yyyy-MM-dd H:mm:ss") : String.Empty;
                lvi.SubItems[1].Text = starts;
                lvi.SubItems[2].Text = ends;
                lvi.SubItems[3].Text = _loader.Status.GetStringValue();
            }
            if (typeof(CDMTimer) == loader.GetType())
            {
                var _loader = (CDMTimer)loader;
                if (null == lvi) return;
                var starts = Status.STARTED == _loader.Status && _loader.StartTime.HasValue ? _loader.StartTime.Value.ToString("yyyy-MM-dd H:mm:ss") : String.Empty;
                var ends = Status.COMPLETED == _loader.Status && _loader.EndTime.HasValue ? _loader.EndTime.Value.ToString("yyyy-MM-dd H:mm:ss") : String.Empty;
                lvi.SubItems[1].Text = starts;
                lvi.SubItems[2].Text = ends;
                lvi.SubItems[3].Text = _loader.Status.GetStringValue();
            }
        }

        private ListViewItem fromCollection(ListView view, long tag_id)
        {
            for (var i = 0; i < view.Items.Count; i++)
            {
                var lvi = (ListViewItem)view.Items[i];
                if ((long)lvi.Tag == tag_id) return lvi;
            }
            return null;
        }

        private void lvChunks_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            selectedOrdinal = lvChunks.SelectedIndices.Count <= 0 ? null : (int?)Int64.Parse(lvChunks.SelectedItems[0].SubItems[5].Text);
            queryLoaded = false;
            lvQuery.Items.Clear();
            loadQueries();
            // Console.WriteLine($"Ordinal Selected: #{selectedOrdinal}");
        }

        private void lvQuery_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var sel = lvQuery.SelectedIndices.Count <= 0 ? null : (int?)Int64.Parse(lvQuery.SelectedItems[0].SubItems[4].Text);
            //if (null != sel) (new RuntimeView(SysDB<CDMTimer>.Load("Where Id = @Id", new { Id = sel }))).ShowDialog();
        }

        private void lvChunks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var sel = lvChunks.SelectedIndices.Count <= 0 ? null : (int?)Int64.Parse(lvChunks.SelectedItems[0].SubItems[4].Text);
            //if (null != sel) (new RuntimeView(SysDB<ChunkTimer>.Load("Where Id = @Id", new { Id = sel }))).ShowDialog();
        }
    }
}
