using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace WindowsFormsAppTest
{
    public partial class RuntimeView : Form
    {
        AbsTable absTable;
        public RuntimeView()
        {
            InitializeComponent();
        }

        public RuntimeView(AbsTable loader) : this()
        {
            absTable = loader;
            if (null == absTable) return;
            switch (absTable.GetType().Name.ToLower())
            {
                case "chunktimer": loadChunk(); break;
                case "cdmtimer": loadQuery(); break;
            }
        }

        private void loadChunk()
        {
            this.Text = "Chunk View";
            var chunk = (ChunkTimer)absTable;
            lbName.Text = $"[#{chunk.Id} WorkLoad:{chunk.WorkLoadId}]";
            lbStart.Text = new Status[] { Status.STARTED, Status.COMPLETED }.Contains(chunk.Status) && chunk.StartTime.HasValue ? chunk.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : String.Empty;
            lbEnd.Text = new Status[] { Status.COMPLETED }.Contains(chunk.Status) && chunk.EndTime.HasValue ? chunk.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : String.Empty;
            lbStatus.Text = chunk.Status.GetStringValue();
            lbChunkId.Text = chunk.ChunkId.ToString();
            tbError.Text = chunk.ErrorLog;
            tbQuery.Text = "[MULTIPLE QUERIES]";
        }

        private void loadQuery()
        {
            this.Text = "Query View";
            var timer = (CDMTimer)absTable;
            lbName.Text = timer.Name;
            lbStart.Text = new Status[] { Status.STARTED, Status.COMPLETED }.Contains(timer.Status) && timer.StartTime.HasValue ? timer.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : String.Empty;
            lbEnd.Text = new Status[] { Status.COMPLETED }.Contains(timer.Status) && timer.EndTime.HasValue ? timer.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : String.Empty;
            lbStatus.Text = timer.Status.GetStringValue();
            lbChunkId.Text = timer.ChunkId.ToString();
            tbError.Text = timer.ErrorLog;
            tbQuery.Text = timer.Query;
        }
    }
}
