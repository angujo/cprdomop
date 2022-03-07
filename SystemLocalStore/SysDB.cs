using System;
using System.Collections.Generic;
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

        public static bool Delete(Object parameters)
        {
            return DataAccess.Delete(typeof(T).Name, parameters);
        }
        public static bool Delete(string condition, Object parameters)
        {
            return DataAccess.Delete(typeof(T).Name, condition, parameters);
        }
        public static T Load(string conditions = null, Object parameters = null)
        {
            return DataAccess.Load<T>(typeof(T).Name, conditions, parameters);
        }
        public static bool Exists(string conditions = null, Object parameters = null)
        {
            return DataAccess.Exists(typeof(T).Name, conditions, parameters);
        }
        public static int RunQuery(string sql, Object parameters = null)
        {
            return DataAccess.Query(sql, parameters);
        }

        public static T LoadOrNew(string conditions = null, Object parameters = null)
        {
            var ld = Load(conditions, parameters);
            if (null != ld) return ld;
            return (T)(Activator.CreateInstance(typeof(T))).SetProperties(parameters);
        }

        public static List<T> List(string conditions = null, Object parameters = null)
        {
            return DataAccess.LoadList<T>(conditions, parameters);
        }

        public static List<C> Column<C>(string col_name, string conditions = null, Object parameters = null)
        {
            return DataAccess.Column<C>(col_name, typeof(T).Name, conditions, parameters);
        }

    }
}
