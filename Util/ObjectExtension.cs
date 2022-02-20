using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Windows.Forms;

namespace Util
{
    public static class ObjectExtension
    {
        public static ExpandoObject SetProperties(this ExpandoObject obj, Object parameters)
        {
            if (parameters == null) return obj;
            AssignProps(parameters, (name, value) =>
            {
                ((IDictionary<string, object>)obj).Add(name, value);
            });
            return obj;
        }

        private static void AssignProps(Object parameters, Action<string, object> func)
        {
            if (parameters.GetType().Name.Contains("AnonymousType"))
            {
                var properties = parameters.GetType().GetProperties();
                foreach (var property in properties)
                {
                    func(property.Name, property.GetValue(parameters));
                }
            }
            else if (parameters.GetType().Name.Equals("ExpandoObject"))
            {
                var _test = (IDictionary<string, object>)parameters;
                foreach (var descriptor in _test.Keys)
                {
                    func(descriptor, _test[descriptor]);
                }
            }
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
