using FileProcessor;
using System;
using System.Windows.Forms;
using SystemLocalStore;
using SystemLocalStore.models;

namespace WindowsFormsAppTest
{
    public partial class SourceProcessControl : UserControl
    {
        WorkLoad workLoad;
        public SourceProcessControl()
        {
            InitializeComponent();
        }

        public void SetWorkLoad(WorkLoad wl) { workLoad = wl; }

        private void btnLoad_Click(object sender, System.EventArgs e)
        {
            try
            {
                DBSchema source = DataAccess.loadSchema(workLoad, "source");
                DBSchema voc = DataAccess.loadSchema(workLoad, "vocabulary");
                if (null == source || null == voc) throw new Exception("Ensure both source and vocabulary schemas are set!");
                AbsDBMSProcessor processor = FileDBProcessor.GetProcessor(DataAccess.loadSourceFiles(workLoad).ToArray(), source, voc);
                var queues = processor.GetQueue();
                lvQueue.Items.Clear();
                foreach (var q in queues)
                {
                    ListViewItem listViewItem = new ListViewItem($"{q.TaskIndex}-{q.ParalellIndex}-{q.Ordinal??0}", 0);
                    listViewItem.SubItems.Add(q.FilePath);
                    listViewItem.SubItems.Add("0%");
                    lvQueue.Items.Add(listViewItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }

        }
    }
}
