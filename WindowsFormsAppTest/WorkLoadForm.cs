using System;
using System.Windows.Forms;
using SystemLocalStore;
using SystemLocalStore.models;

namespace WindowsFormsAppTest
{
    public partial class WorkLoadForm : Form
    {
        WorkLoad workLoad;
        public WorkLoadForm(WorkLoad wl)
        {
            InitializeComponent();
            workLoad = wl;
            tbName.DataBindings.Add("Text", workLoad, "Name");
            dtDate.DataBindings.Add("Value", workLoad, "ReleaseDate");
            cbSourceProcessed.DataBindings.Add("Checked", workLoad, "SourceProcessed");
        }

        private void Save()
        {
            workLoad.FilesLocked = workLoad.SourceProcessed;
            Int64 Id = DataAccess.InsertOrUpdate(workLoad);
            if (0 >= workLoad.Id) workLoad.Id = Id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            Save();
            this.Dispose();
        }

        private void btnSL_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            Save();
            this.Dispose();
        }
    }
}
