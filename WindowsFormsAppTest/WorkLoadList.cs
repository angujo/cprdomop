using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppTest
{
    public partial class WorkLoadList : Form
    {
        public int selId { get; private set; }
        public WorkLoadList()
        {
            InitializeComponent();
        }

        public WorkLoadList addItem(ListViewItem item)
        {
            lvLoads.Items.Add(item); return this;
        }

        private void lvLoads_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            loadSelected(); this.DialogResult = DialogResult.OK;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadSelected();
        }

        private bool loadSelected()
        {
            if (lvLoads.SelectedIndices.Count <= 0) return false;
            selId = (int)Int64.Parse(lvLoads.SelectedItems[0].SubItems[2].Text);
            return true;
        }

        private void lvLoads_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            btnLoad.Enabled = lvLoads.SelectedIndices.Count > 0;
        }
    }
}
