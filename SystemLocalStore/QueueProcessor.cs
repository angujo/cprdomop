using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore.models;
using Util;

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
            object f(dynamic i, object oV) { return oV.SetProperties(parameters); }

            theList.AddOrUpdate(id, createObject<T>(id, parameters), (Func<dynamic, object, object>)f);

            return this;
        }

        private Object createObject<T>(dynamic key, Object parameters)
        {
            var obj = (T)(null == creator ? Activator.CreateInstance(typeof(T)) : creator(key));
            return obj.SetProperties(parameters);// populateObject(obj, parameters);
        }

        public Task Open()
        {
            return Task.Run(() =>
            {
                while (false == closeRequested)
                {
                    var keys = theList.Keys.ToArray();
                    if (keys.Length <= 0)
                    {
                        Task.Delay(500);
                        continue;
                    }
                    var key = keys[(new Random()).Next(keys.Length)];
                    if (!theList.TryGetValue(key, out Object abs)) continue;
                    try
                    {
                        if (typeof(AbsUpsTable).IsAssignableFrom(abs.GetType())) ((AbsUpsTable)abs).UpSert();
                        else if (typeof(AbsTable).IsAssignableFrom(abs.GetType())) ((AbsTable)abs).Save();
                        theList.TryRemove(key, out abs);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"We missed an update for {key}");
                        Console.WriteLine(ex.StackTrace);
                        throw;
                    }

                }
            });
        }

        public void Close() { closeRequested = true; }

        public static QueueProcessor Add<T>(dynamic id, Object parameters)
        {
            return getMe<T>().AddOrUpdate<T>(id, parameters);
        }

        public static QueueProcessor Timed<T>(dynamic id, Action act, Object startParams = null, Object endParams = null)
        {
            Add<T>(id, (startParams ?? new { }).SetProperties(new { StartTime = DateTime.Now }));
            act();
            return Add<T>(id, (endParams ?? new { }).SetProperties(new { EndTime = DateTime.Now }));
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

        public static void SetCreator<T>(Func<dynamic, Object> creator)
        {
            getMe<T>().creator = creator;
        }
    }
}
