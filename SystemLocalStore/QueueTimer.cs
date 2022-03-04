using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

        public static void Timexxx(T obj, object key, Action func, object preParams = null, object postParams = null)
        {
            var pra = ObjectExtension.AnObject(preParams).SetProperties(new { StartTime = DateTime.Now, EndTime = DateTime.Now, Status = Status.STARTED });
            var item = getMe().AddOrUpdate(key, null);
            item.SetProperties(pra);
            try
            {
                if (null != obj) item.SetItem(obj);
                item.Commit();
                func();
                pra.SetProperties(new { Status = Status.COMPLETED, });
            }
            catch (Exception ex)
            {
                pra.SetProperties(new { ErrorLog = ex.Message, Status = Status.ERROR_EXIT, });
                throw;
            }
            finally
            {
                pra.SetProperties(new { EndTime = DateTime.Now, }).SetProperties(postParams);
                item = getMe().AddOrUpdate(key, null);
                item.SetProperties(pra);
                if (null != obj) item.SetItem(obj);

            }
        }
        public static void Time(T obj, object key, Action func, object preParams = null, object postParams = null)
        {
            var pra = ObjectExtension.AnObject(preParams).SetProperties(new { StartTime = DateTime.Now, EndTime = DateTime.Now, Status = Status.STARTED });
            if(null==obj)obj= (T)Activator.CreateInstance(typeof(T));
            obj.SetProperties(pra);
            try
            {
                obj.UpSert();
                func();
                pra = ObjectExtension.AnObject(postParams).SetProperties(new { Status = Status.COMPLETED, });
            }
            catch (Exception ex)
            {
                pra.SetProperties(new { ErrorLog = ex.Message, Status = Status.ERROR_EXIT, });
                Logger.Error(typeof(T).Name);
                Logger.Exception(ex);
            }
            finally
            {
                obj.SetProperties(pra).SetProperties(new { EndTime = DateTime.Now, });
                obj.UpSert();
            }
        }

        public static void Time(object key, Action func, object preParams = null, object postParams = null)
        {
            Time(null, key, func, preParams, postParams);
        }

        private T SetLoader(T obj)
        {
            var loadKeys = ObjectExtension.AnObject();
            var cols = (string[])typeof(T).GetMethod("UpsColumns").Invoke(null, null);
            // var props=
            var cond = new List<string>();
            if (null != obj)
            {
                obj.RunProperties((name, value) =>
               {
                   if (!cols.Contains(name)) return;
                   loadKeys.SetProperties(name, value);
                   cond.Add($"{name} = @{name}");
               });
            }
            return SysDB<T>.LoadOrNew((cond.Count > 0 ? "Where " : string.Empty) + string.Join(" AND ", cond), loadKeys);
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
                        continue;
                    }
                    var key = keys[(new Random()).Next(keys.Length)];
                    if (!theList.TryRemove(key, out QueueItem<T> abs)) continue;
                    try
                    {
                        abs.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"We missed an update for {key}");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        //    throw;
                    }

                }
            });
        }

        protected QueueItem<T> AddOrUpdate(dynamic id, object parameters)
        {
            // QueueItem<T> f(dynamic i, QueueItem<T> oV) { return oV.SetProperties(parameters).SetProperties(new { IsScheduled = false }); }
            if (theList.TryRemove(id, out QueueItem<T> abs))
            {
                abs = abs.SetProperties(parameters);
            }
            else
            {
                abs = createObject(parameters);
            }
            abs.SetProperties(new { IsScheduled = false });
            theList.TryAdd(id, abs);
            // theList.AddOrUpdate(id, createObject(parameters), (Func<dynamic, QueueItem<T>, QueueItem<T>>)f);

            return abs;// theList.TryGetValue(id, out QueueItem<T> rt) ? rt : null;
        }

        private QueueItem<T> createObject(Object parameters)
        {
            return (new QueueItem<T>()).SetProperties(parameters);
        }

        private static QueueTimer<T> getMe(bool loadOnly = false)
        {
            var name = typeof(T).Name;
            QueueTimer<T> process;
            if (null == processes)
            {
                if (loadOnly) return null;
                processes = new ConcurrentDictionary<string, QueueTimer<T>>();
            }

            if (!processes.TryGetValue(name, out process))
            {
                if (loadOnly) return null;
                process = new QueueTimer<T>();
                processes.TryAdd(name, process);
                // process.Open();
            }
            return process;
        }

        public static void Close()
        {
            var m = getMe(true);
            if (null == m) return;
            m.closeRequested = true;
        }
    }
}
