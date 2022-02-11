using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppTest.utils
{
    internal static class Util
    {
        public static List<Control> containerControls(object container)
        {
            List<Control> controls = new List<Control>();
            containerControls(container, cntr => { controls.Add(cntr); });
            return controls;
        }

        public static void containerControls(object container, Action<Control> func)
        {
            if (!((Control)container).HasChildren) return;
            foreach (Control control in ((Control)container).Controls)
            {
                if (control.HasChildren) containerControls(control, func);
                else func(control);
            }
        }

        public static string LogLine(string txt)
        {
            if (string.IsNullOrEmpty(txt)) return Environment.NewLine;
            return "[" + DateTime.Now.ToString() + "] " + txt + Environment.NewLine;
        }
    }
}
