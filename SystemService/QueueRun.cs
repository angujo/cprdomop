﻿using DatabaseProcessor;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SystemLocalStore;
using SystemLocalStore.models;

namespace SystemService
{
    public class QueueRun
    {
        private ConcurrentDictionary<int, AbsTable> updates = new ConcurrentDictionary<int, AbsTable>();
        private Dictionary<Int64, AbsDBMSystem> db = new Dictionary<Int64, AbsDBMSystem>();
        public static void DoRun()
        {
            if (Current.IsRunning() || Current.StopRequested()) return;
            Current.workQueue = WorkQueue.NextAvailable();
            if (Current.workQueue == null || !Current.workQueue.Exists()) return;
            Task.Run(() =>
            {
                Current.status = Status.RUNNING;
                (new QueueRun()).SetTasks();
                Current.status = Status.STOPPED;
            });
        }

        public void SetTasks()
        {
            workStarted();
            Task.Run(async () =>
            {
                while (Status.RUNNING == Current.status)
                {
                    await Task.Delay(200);
                    if (updates.Count > 0)
                    {
                        var keys = updates.Keys.ToArray();
                        if (!updates.TryRemove(keys[(new Random()).Next(keys.Length)], out AbsTable queue)) return;
                        queue.InsertOrUpdate();
                        Console.WriteLine($"RUN ABS[{queue.Id}]...{DateTime.Now}");
                    }
                }
            });
            Queue[] queues = Queue.List<Queue>(new { WorkQueueId = Current.workQueue.Id }).ToArray();
            if (queues.Length <= 0) return;
            List<int?> taskIndexes = (queues.Select(q => q.TaskIndex).ToArray() ?? (new int?[] { })).Distinct().ToList();
            List<Task> tasks = new List<Task>();
            foreach (var taskIndex in taskIndexes)
            {
                if (Current.StopRequested()) break;
                TaskIndexRun(queues.Where(q => taskIndex == q.TaskIndex).ToList());
            }
            workStopped();
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
                queueStarted(queue);
                if (Current.StopRequested()) break;
                try
                {
                    Debug.WriteLine($"Executing Queue: {queue.Id} :: {queue.TaskIndex}-{queue.ParallelIndex}-{queue.Ordinal}");
                    runQueue(queue);
                }
                catch (Exception ex)
                {
                    queue.ErrorLog = ex.ToString();
                    queue.Status = Status.ERROR_EXIT;
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    queueStopped(queue);
                }

            }
        }

        private void workStarted()
        {
            Task.Run(() =>
            {
                Current.workQueue.StartTime = DateTime.Now;
                Current.workQueue.Status = Status.STARTED;
                Current.workQueue.InsertOrUpdate();
            });
        }
        private void workStopped()
        {
            Task.Run(() =>
            {
                Current.workQueue.EndTime = DateTime.Now;
                Current.workQueue.Status = Status.STOPPED;
                Current.workQueue.InsertOrUpdate();
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
                var schema = DBSchema.Load<DBSchema>(new { Id = queue.DBSchemaId });
                if (null == schema) return;
                db[scId] = DBMSystem.GetDBMSystem(schema);
            }
            Console.WriteLine($"CONTENT: [{queue.FileContent}]");
            db[scId].RunQuery(queue.FileContent);
        }
    }
}
