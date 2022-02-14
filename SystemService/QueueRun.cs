using DatabaseProcessor;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore;
using SystemLocalStore.models;

namespace SystemService
{
    public class QueueRun
    {
        private ConcurrentBag<AbsTable> updates = new ConcurrentBag<AbsTable>();
        private Dictionary<int, AbsDBMSystem> db = new Dictionary<int, AbsDBMSystem>();
        public static void DoRun()
        {
            if (Current.IsRunning() || Current.StopRequested()) return;
            Current.workQueue = WorkQueue.NextAvailable();
            if (Current.workQueue == null || !Current.workQueue.Exists()) return;
            Current.status = Status.RUNNING;
            Task.Run(() => { (new QueueRun()).SetTasks(); Current.status = Status.STOPPED; });
        }

        public void SetTasks()
        {
            Debug.WriteLine("Am Just testing!");
            workStarted();
            Queue[] queues = Queue.List<Queue>(new { WorkQueueId = Current.workQueue.Id }).ToArray();
            if (queues.Length <= 0) return;
            List<int?> taskIndexes = (queues.Select(q => q.TaskIndex).ToArray() ?? (new int?[] { })).Distinct().ToList();
            List<Task> tasks = new List<Task>();
            foreach (var taskIndex in taskIndexes)
            {
                if (Current.StopRequested()) break;
                TaskIndexRun(queues.Where(q => taskIndex == q.TaskIndex).ToList());
            }
            runConcurrents();
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
            queue.StartTime = DateTime.Now;
            queue.Status = Status.RUNNING;
        }

        private void queueStopped(Queue queue)
        {
            Task.Run(() =>
            {
                queue.EndTime = DateTime.Now;
                if (queue.Status == Status.RUNNING || queue.Status == Status.STARTED) queue.Status = Status.STOPPED;
                updates.Add(queue);
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

        private void runConcurrents()
        {
            foreach (var queue in updates) queue.InsertOrUpdate();
            updates = new ConcurrentBag<AbsTable>();
        }
    }
}
