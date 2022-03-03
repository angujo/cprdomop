using DatabaseProcessor;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace OMOPService
{
    public class QueueRun
    {
        private ConcurrentDictionary<int, AbsTable> updates = new ConcurrentDictionary<int, AbsTable>();
        private Dictionary<Int64, AbsDBMSystem> db = new Dictionary<Int64, AbsDBMSystem>();

        protected QueueRun() { }

        public static void DoRun(WorkQueue wQueue)
        {
            if (Current.IsRunning() || Current.StopRequested() || Current.workQueue == null || !Current.workQueue.Exists() || QAction.SOURCE_FILE != wQueue.QueueType) return;
            Current.workQueue = wQueue;
            Task.Run(() =>
            {
                Current.status = Status.RUNNING;
                (new QueueRun()).SetTasks();
                Current.status = Status.STOPPED;
            });
        }

        public void SetTasks()
        {
            Logger.Info("WorkQueue<QueueTime> Starter: QueueRun");
            QueueTimer<WorkQueue>.Time(Current.workQueue, Current.workQueue.Id, () =>
            {
                Queue[] queues = SysDB<Queue>.List("Where WorkQueueId = @WorkQueueId",new { WorkQueueId = Current.workQueue.Id }).ToArray();
                if (queues.Length <= 0) return;
                List<int?> taskIndexes = (queues.Select(q => q.TaskIndex).ToArray() ?? (new int?[] { })).Distinct().ToList();
                foreach (var taskIndex in taskIndexes)
                {
                    if (Current.StopRequested())
                    {
                        Current.workQueue.Status = Status.STOPPED;
                        return;
                    }
                    TaskIndexRun(queues.Where(q => taskIndex == q.TaskIndex).ToList());
                }
                Current.workQueue.Status = Status.COMPLETED;
                var wl = SysDB<WorkLoad>.Load("Where Id = @Id",new { Id = Current.workQueue.WorkLoadId });
                if (null != wl)
                {
                    wl.SourceProcessed = true;
                    wl.Save();
                }
            }, new { Status = Status.STARTED });
        }

        protected void TaskIndexRun(List<Queue> queues)
        {
            var parallelIndexes = (queues.Select(q => q.ParallelIndex).ToArray() ?? (new int?[] { })).Distinct().ToList();
            Parallel.ForEach(parallelIndexes, pIndex =>
            {
                Console.WriteLine($"Starting Parallel {pIndex}");
                queues.Sort((fQ, sQ) => fQ.Ordinal.CompareTo(sQ.Ordinal));
                ParalellIndexRun(queues.Where(p => p.ParallelIndex == pIndex).ToArray());
            });
        }

        protected void ParalellIndexRun(Queue[] queues)
        {
            foreach (var queue in queues)
            {
                if (Current.StopRequested()) break;
                QueueTimer<Queue>.Time(queue, queue.Id,
                    () =>
                    {
                        try
                        {
                            Debug.WriteLine($"Executing Queue: {queue.Id} :: {queue.TaskIndex}-{queue.ParallelIndex}-{queue.Ordinal}");
                            runQueue(queue);
                            queue.Status = Status.STOPPED;
                        }
                        catch (Exception ex)
                        {
                            queue.ErrorLog = ex.ToString();
                            queue.Status = Status.ERROR_EXIT;
                            Logger.Exception(ex);
                            Console.WriteLine(ex.ToString());
                        }
                    },
                    new { Status = Status.RUNNING });
            }
        }

        private void workStarted()
        {
            Task.Run(() =>
            {
                Current.workQueue.StartTime = DateTime.Now;
                Current.workQueue.Status = Status.STARTED;
                Current.workQueue.Save();
            });
        }
        private void workStopped()
        {
            Task.Run(() =>
            {
                Current.workQueue.EndTime = DateTime.Now;
                Current.workQueue.Status = Status.STOPPED;
                Current.workQueue.Save();
            });
        }

        private void queueStarted(Queue queue)
        {
            Task.Run(() =>
            {
                updates.AddOrUpdate((int)queue.Id, queue, (i, a) =>
                {
                    var q = (Queue)a;
                    q.StartTime = DateTime.Now;
                    q.Status = Status.RUNNING;
                    return q;
                });
            });
        }

        private void queueStopped(Queue queue)
        {
            Task.Run(() =>
            {
                updates.AddOrUpdate((int)queue.Id, queue, (i, a) =>
                {
                    var q = (Queue)a;
                    q.EndTime = DateTime.Now;
                    if (q.Status == Status.RUNNING || q.Status == Status.STARTED) q.Status = Status.STOPPED;
                    return q;
                });
            });
        }

        private void runQueue(Queue queue)
        {
            var scId = (int)queue.DBSchemaId;
            if (!db.ContainsKey(scId))
            {
                var schema = SysDB<DBSchema>.Load("Where Id = @Id",new { Id = queue.DBSchemaId });
                if (null == schema) return;
                db[scId] = DBMSystem.GetDBMSystem(schema);
            }
            //  Console.WriteLine($"CONTENT: [{queue.FileContent}]");
            db[scId].RunQuery(queue.FileContent);
        }
    }
}
