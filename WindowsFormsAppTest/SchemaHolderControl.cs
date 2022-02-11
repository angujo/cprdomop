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
    public partial class SchemaHolderControl : UserControl
    {
        public SchemaHolderControl(WorkLoad workLoad)
        {
            InitializeComponent();
            dsSource.loadSchema("source", workLoad);
            dsVocabulary.loadSchema("vocabulary", workLoad);
            dsTarget.loadSchema("target", workLoad);
        }
    }
}
