using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Windows.Forms;

namespace Util
{
    public static class ObjectExtension
    {
        public static bool HasMethod(this object obj, string name)
        {
            try
            {
                return null != obj.GetType().GetMethod(name);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static ExpandoObject ShadowCopy(this ExpandoObject obj)
        {
            var origObj = (IDictionary<string, object>)obj;
            var cloneObj = new ExpandoObject();

            var kys = new List<object>(origObj.Keys);
            foreach (string descriptor in kys)
            {
                ((IDictionary<string, object>)cloneObj)[descriptor] = origObj[descriptor];
            }
            return cloneObj;
        }

        public static Object GetDefault<T>(this T obj)
        {
            return GetDefault(typeof(T));
        }
        public static ExpandoObject AnObject(Object parameters = null)
        {
            return (new ExpandoObject()).SetProperties(parameters);
        }

        public static void RunProperties<T>(this T obj, Action<string, object> func)
        {
            loopProperties(obj, func);
        }

        public static T SetProperties<T>(this T obj, string name, object value = null)
        {
            setValue(obj, name, value);
            return (T)obj;
        }

        public static T SetProperties<T>(this T obj, Object parameters)
        {
            if (parameters == null) return obj;
            AssignProps(parameters, (name, value) =>
            {
                setValue(obj, name, value);
            });
            return (T)obj;
        }

        public static object GetProperty<T>(this T obj, string name)
        {
            return null == obj ? null : getValue(obj, name);
        }

        public static bool HasProperty<T>(this T obj, string name)
        {
            return obj.GetType().Name.Equals("ExpandoObject") ? ((IDictionary<string, object>)obj).ContainsKey(name) : null != obj.GetType().GetProperty(name);
        }

        private static void setValue(object obj, string name, object value)
        {
            if (obj.GetType().Name.Equals("ExpandoObject"))
            {
                var dict = ((IDictionary<string, object>)obj);
                dict[name] = value;
            }
            else
            {
                PropertyInfo pi = obj.GetType().GetProperty(name);
                if (null == pi) return;
                pi.SetValue(obj, value);
            }
        }

        private static object getValue(object obj, string name)
        {
            if (obj.GetType().Name.Equals("ExpandoObject"))
            {
                var dict = ((IDictionary<string, object>)obj);
                return dict.ContainsKey(name) ? dict[name] : null;
            }
            else
            {
                PropertyInfo pi = obj.GetType().GetProperty(name);
                return (null == pi) ? null : pi.GetValue(obj);
            }
        }

        private static void AssignProps(Object parameters, Action<string, object> func)
        {
            loopProperties(parameters, func);
        }

        private static void loopProperties(Object parameters, Action<string, object> func)
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
                var copyObj = ((ExpandoObject)parameters).ShadowCopy();
                var kys = new List<object>(((IDictionary<string, object>)copyObj).Keys);
                foreach (string descriptor in kys)
                {
                    func(descriptor, ((IDictionary<string, object>)copyObj)[descriptor]);
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
        public static object GetDefault(Type type)
        {
            // If no Type was supplied, if the Type was a reference type, or if the Type was a System.Void, return null
            if (type == null || !type.GetTypeInfo().IsValueType || type == typeof(void))
                return null;

            // If the supplied Type has generic parameters, its default value cannot be determined
            if (type.ContainsGenericParameters)
                throw new ArgumentException(
                    "{" + MethodInfo.GetCurrentMethod() + "} Error:\n\nThe supplied value type <" + type +
                    "> contains generic parameters, so the default value cannot be retrieved");

            // If the Type is a primitive type, or if it is another publicly-visible value type (i.e. struct), return a 
            //  default instance of the value type
            if (type.GetTypeInfo().IsPrimitive || !type.GetTypeInfo().IsNotPublic)
            {
                try
                {
                    return Activator.CreateInstance(type);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(
                        "{" + MethodInfo.GetCurrentMethod() + "} Error:\n\nThe Activator.CreateInstance method could not " +
                        "create a default instance of the supplied value type <" + type +
                        "> (Inner Exception message: \"" + e.Message + "\")", e);
                }
            }

            // Fail with exception
            throw new ArgumentException("{" + MethodInfo.GetCurrentMethod() + "} Error:\n\nThe supplied value type <" + type +
                "> is not a publicly-visible type, so the default value cannot be retrieved");
        }
    }
}
