using DatabaseProcessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace OMOPProcessor
{
    public class TimerLogger
    {
        Dictionary<string, CDMTimer> nonChunkTimers = new Dictionary<string, CDMTimer>();
        WorkLoad workLoad;
        Dictionary<int, Dictionary<string, CDMTimer>> chunkTimers = new Dictionary<int, Dictionary<string, CDMTimer>>();
        readonly string[] NonChunks = new string[] { "ChunkLoad", "CareSite", "CdmSource", "ChunkSetup", "CohortDefinition", "CreateTables", "Location", "Provider", "SourceToSource", "SourceToStandard", "VisitDetailUpdate", "VisitOccurrenceUpdate", };
        string[] methodNames;

        public TimerLogger(WorkLoad wl)
        {
            workLoad = wl;
            methodNames = typeof(Script).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Select(m => m.Name).ToArray();
            loadNonChunks();
        }

        protected void loadNonChunks()
        {
            Logger.Info("Loading Non Chunk Timers...");
            loopMethods((name, cTimer) =>
            {
                nonChunkTimers.Add(name, GetTimer(name, -1, cTimer));
            });
            Logger.Info("DONE Loading Non Chunk Timers");
        }

        public void prepareCDMTimers(ChunkTimer chunkTimer) { loadForChunk(chunkTimer); }

        public bool RunCDMTimer(string name, int ChunkId, Action<string> runFunc)
        {

            if (!chunkTimers.ContainsKey(ChunkId)) return true;
            var timer = chunkTimers[ChunkId].ContainsKey(name) ? chunkTimers[ChunkId][name] : null;
            if (null == timer) return true;
            if (timer.Status == Status.STARTED && FileExist(name) && FileExist(name, true))
            {
                runFunc($"clean-{name}"); return true;
            }
            return Status.COMPLETED != timer.Status;
        }

        public void updateTimer(int chunkId, string name, Status status)
        {
            if (!chunkTimers.ContainsKey(chunkId) || !chunkTimers[chunkId].ContainsKey(name)) return;
            chunkTimers[chunkId][name].Status = status;
        }

        protected void loadForChunk(ChunkTimer chunk)
        {
            Dictionary<string, CDMTimer> cHolder = new Dictionary<string, CDMTimer>();
            loopMethods((name, cTimer) =>
            {
                cHolder.Add(name, GetTimer(name, chunk.ChunkId, cTimer));
            }, chunk.ChunkId);

            chunkTimers[chunk.ChunkId] = cHolder;
        }

        protected void loopMethods(Action<string, CDMTimer> func, int chunkId = -1)
        {
            List<CDMTimer> timers = SysDB<CDMTimer>.List($"Where WorkLoadId = @WLId AND ChunkId = @CId AND Name{(-1 != chunkId ? " !" : string.Empty)}= ANY(@Names)", new { WLId = workLoad.Id, CId = chunkId, Names = NonChunks });
            foreach (string name in methodNames)
            {
                if ((NonChunks.Contains(name) && -1 != chunkId) || (!NonChunks.Contains(name) && -1 == chunkId)) continue;
                func(name, timers.Find(t => t.Name.Equals(name)));
            }
        }

        private bool FileExist(string name, bool isUndo = false)
        {
            string p = Path.Combine(Setting.InstallationDirectory, "omopscripts", DBMSIdentifier.Active(), isUndo ? "undo" : string.Empty, isUndo ? UndoFileName(name) : DoFileName(name));
            return File.Exists(p);
        }

        private string DoFileName(string name)
        {
            return $"{name.ToKebabCase()}.sql";
        }
        private string UndoFileName(string name)
        {
            return $"clean-{name.ToKebabCase()}.sql";
        }

        private CDMTimer GetTimer(string name, int chunkId = -1, CDMTimer c_timer = null)
        {
            if (c_timer == null) c_timer = new CDMTimer { Name = name, ChunkId = chunkId, WorkLoadId = (long)workLoad.Id };
            if (!c_timer.Exists() || Status.COMPLETED != c_timer.Status)
            {
                c_timer.Status = Status.QUEUED;
                c_timer.UpSert();
            }
            return c_timer;
        }
    }
}
