using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore.models;
using Util;

namespace SystemLocalStore
{
    public static class SysDB<T> where T : AbsTable
    {
        public static void InsertOrUpdate(List<T> items)
        {
            foreach (var item in items) DataAccess.InsertOrUpdate(item);
        }

        public static bool Delete(Object parameters = null)
        {
            return DataAccess.Delete(typeof(T).Name, parameters);
        }
        public static T Load(Object parameters = null)
        {
            return DataAccess.Load<T>(typeof(T).Name, parameters);
        }

        public static T LoadOrNew(Object parameters = null)
        {
            var ld = Load(parameters);
            if (null != ld) return ld;
            return (T)(Activator.CreateInstance(typeof(T))).SetProperties(parameters);
        }

        public static List<T> List(Object parameters = null)
        {
            return DataAccess.LoadList<T>(typeof(T).Name, parameters);
        }

    }
}
