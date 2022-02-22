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
            if (load.SourceProcessed)
            {
                tabsHolder.TabPages.Remove(tabSource);
                tabsHolder.TabPages.Remove(tabConf);
            }
            else
            {
                addTabPage(pnConf, new ConfigControl(workl));
                addTabPage(pnSource, new SourceProcessControl(workl));
            }

            addTabPage(pnCdm, new CDMControl(workl));
            addTabPage(pnDB, new SchemaHolderControl(workl));
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
