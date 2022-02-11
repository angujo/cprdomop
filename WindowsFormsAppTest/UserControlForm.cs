using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemStorage.models;

namespace WindowsFormsAppTest
{
    public partial class UserControlForm : UserControl
    {
        private WorkLoad load;

        public UserControlForm()
        {
            InitializeComponent();

        }
        public UserControlForm(WorkLoad workl) : this()
        {
            load = workl;
            loadTitle.Text = workl.Name;

            addTabPage(pnConf, new ConfigControl(workl));
            Task.Run(() =>
            {
                addTabPage(pnDB, new SchemaHolderControl(workl));
            });
        }

        private void addTabPage(Panel pg, UserControl userControl)
        {
            pg.Controls.Clear();
            userControl.AutoScroll = true;
            //userControl.Dock = DockStyle.Fill;
            pg.Controls.Add(userControl);
        }

        public bool IsLoaded(WorkLoad work)
        {
            return work.Id.Equals(load.Id);
        }
    }
}
