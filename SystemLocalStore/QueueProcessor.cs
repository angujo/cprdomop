using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore.models;

namespace SystemLocalStore
{

    public class QueueProcessor
    {
        static Dictionary<string, QueueProcessor> processes = new Dictionary<string, QueueProcessor>();

        ConcurrentDictionary<dynamic, Object> theList = new ConcurrentDictionary<dynamic, Object>();
        bool closeRequested = false;
        Func<dynamic, Object> creator;

        public QueueProcessor(Func<dynamic, Object> creatorFunc = null)
        {
            creator = creatorFunc;
        }

        public QueueProcessor Add(dynamic id, Object tValue, Func<dynamic, Object, Object> func = null)
        {
            if (null == func) func = (key, oldValue) => tValue;
            theList.AddOrUpdate(id, tValue, func);
            return this;
        }

        public QueueProcessor AddOrUpdate<T>(dynamic id, Object parameters)
        {
            object f(dynamic i, object oV) { return populateObject(i, oV); }

            theList.AddOrUpdate(id, createObject<T>(id, parameters), (Func<dynamic, object, object>)f);

            return this;
        }

        private Object createObject<T>(dynamic key, Object parameters)
        {
            var obj = (T)(null == creator ? Activator.CreateInstance(typeof(T)) : creator(key));
            return populateObject(obj, parameters);
        }

        private Object populateObject(Object obj, Object parameters)
        {
            var properties = parameters.GetType().GetProperties();
            foreach (var property in properties)
            {
                PropertyInfo propInfo = obj.GetType().GetProperty(property.Name);
                if (null == propInfo) continue;
                propInfo.SetValue(obj, property.GetValue(parameters, null), null);
            }
            return obj;
        }

        public Task Open()
        {
            return Task.Run(() =>
            {
                while (false == closeRequested)
                {
                    var keys = theList.Keys.ToArray();
                    if (!theList.TryRemove(keys[(new Random()).Next(keys.Length)], out Object abs)) return;
                    if (typeof(AbsTable).IsAssignableFrom(abs.GetType())) ((AbsTable)abs).InsertOrUpdate();
                }
            });
        }

        public void Close() { closeRequested = true; }

        public static QueueProcessor Add<T>(dynamic id, Object parameters)
        {
            return getMe<T>().AddOrUpdate<T>(id, parameters);
        }

        public static void CloseInstance<T>() { getMe<T>().Close(); }

        private static QueueProcessor getMe<T>()
        {
            var name = typeof(T).Name;
            if (!processes.TryGetValue(name, out QueueProcessor process))
            {
                processes.Add(name, process = new QueueProcessor());
                process.Open();
            }
            return process;
        }
    }
}
