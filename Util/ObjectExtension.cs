using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Util
{
    public static class ObjectExtension
    {
        public static Object SetProperties(this Object obj,Object parameters)
        {
            var properties = parameters.GetType().GetProperties();
            foreach (var property in properties)
            {
                PropertyInfo propInfo = obj.GetType().GetProperty(property.Name);
                if (null == propInfo) continue;
                propInfo.SetValue(obj, property.GetValue(parameters));
            }
            return obj;
        }
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
