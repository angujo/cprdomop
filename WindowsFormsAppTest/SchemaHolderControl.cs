using System.Windows.Forms;
using SystemLocalStore.models;

namespace WindowsFormsAppTest
{
    public partial class SchemaHolderControl : UserControl
    {
        public SchemaHolderControl(WorkLoad workLoad)
        {
            InitializeComponent();
            sourceProcessControl.SetWorkLoad(workLoad);
            dsSource.loadSchema("source", workLoad);
            dsVocabulary.loadSchema("vocabulary", workLoad);
            dsTarget.loadSchema("target", workLoad);
        }
    }
}
