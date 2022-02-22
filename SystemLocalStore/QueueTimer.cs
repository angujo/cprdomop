using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore.models;
using Util;

namespace SystemLocalStore
{
    public class QueueTimer<T> where T : AbsUpsTable
    {
        static ConcurrentDictionary<string, QueueTimer<T>> processes = new ConcurrentDictionary<string, QueueTimer<T>>();

        private bool closeRequested = false;
        ConcurrentDictionary<dynamic, QueueItem<T>> theList = new ConcurrentDictionary<dynamic, QueueItem<T>>();
        protected QueueTimer() { }

        public static void Time(object key, Action func, object parameters = null)
        {
            var item = getMe().AddOrUpdate(key, new { StartTime = DateTime.Now });
            item.SetProperties(parameters);
            func();
            item.SetProperties(new { EndTime = DateTime.Now });


            //Test
            var i = 1 + 0;
        }

        public Task Open()
        {
            return Task.Run(() =>
            {
                while (false == closeRequested)
                {
                    var schedules = theList.Where(sc => sc.Value.IsScheduled);
                    var keys = schedules.ToList().Select(kv => kv.Key).ToArray();
                    if (keys.Length <= 0)
                    {
                        Task.Delay(500);
                        continue;
                    }
                    var key = keys[(new Random()).Next(keys.Length)];
                    if (!theList.TryGetValue(key, out QueueItem<T> abs)) continue;
                    try
                    {
                        abs.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"We missed an update for {key}");
                        Console.WriteLine(ex.StackTrace);
                    //    throw;
                    }

                }
            });
        }

        protected QueueItem<T> AddOrUpdate(dynamic id, object parameters)
        {
            QueueItem<T> f(dynamic i, QueueItem<T> oV) { return oV.SetProperties(parameters); }

            theList.AddOrUpdate(id, createObject(parameters), (Func<dynamic, QueueItem<T>, QueueItem<T>>)f);

            return theList.TryGetValue(id, out QueueItem<T> rt) ? rt : null;
        }

        private QueueItem<T> createObject(Object parameters)
        {
            return (new QueueItem<T>()).SetProperties(parameters);
        }

        private static QueueTimer<T> getMe()
        {
            var name = typeof(T).Name;
            QueueTimer<T> process;
            if (null == processes) processes = new ConcurrentDictionary<string, QueueTimer<T>>();

            if (!processes.TryGetValue(name, out process))
            {
                process = new QueueTimer<T>();
                processes.TryAdd(name, process);
                process.Open();
            }
            return process;
        }
    }
}
