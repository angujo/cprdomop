using System.Threading.Tasks;
using System.Windows.Forms;
using SystemLocalStore.models;

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
            addTabPage(pnCdm, new CDMControl(workl));
            Task.Run(() =>
            {
                addTabPage(pnDB, new SchemaHolderControl(workl));
                addTabPage(pnSource, new SourceProcessControl(workl));
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
