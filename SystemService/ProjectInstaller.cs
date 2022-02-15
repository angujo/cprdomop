using System.ComponentModel;
using System.Configuration.Install;

namespace SystemService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void ProjectInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
