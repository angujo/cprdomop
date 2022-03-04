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
        string[] NonChunks = new string[] { "ChunkLoad", "CareSite", "CdmSource", "ChunkSetup", "CohortDefinition", "CreateTables", "Location", "Provider", "SourceToSource", "SourceToStandard", "VisitDetailUpdate", "VisitOccurrenceUpdate", };

        public TimerLogger(WorkLoad wl) { workLoad = wl; loadNonChunks(); }

        protected void loadNonChunks()
        {
            loopMethods(name =>
            {
                CDMTimer c;
                if (null != (c = GetTimer(name))) nonChunkTimers.Add(name, c);
            });
        }

        public void prepareCDMTimers(ChunkTimer chunkTimer) { loadForChunk(chunkTimer); }

        public bool RunCDMTimer(string name, ChunkTimer chunk, Action<string> runFunc)
        {
            if (chunkTimers.Count <= 0) { loadForChunk(chunk); }
            return RunCDMTimer(name, chunk.ChunkId, runFunc);
        }
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

        public bool AllCompleted(int ChunkId)
        {
            return chunkTimers.ContainsKey(ChunkId) && 0 == (int)chunkTimers[ChunkId].Where(kv => kv.Value.Status != Status.COMPLETED).Count();
        }

        protected void loadForChunk(ChunkTimer chunk)
        {
            Dictionary<string, CDMTimer> cHolder = new Dictionary<string, CDMTimer>();
            loopMethods(name =>
            {
                CDMTimer c;
                if (null != (c = GetTimer(name, chunk))) cHolder.Add(name, c);
            });
            if (chunkTimers.ContainsKey(chunk.ChunkId))
            {
                chunkTimers[chunk.ChunkId] = cHolder;
            }
            else chunkTimers.Add(chunk.ChunkId, cHolder);
        }

        protected void loopMethods(Action<string> func)
        {
            MethodInfo[] methods = typeof(Script).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo method in methods)
            {
                func(method.Name);
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

        private CDMTimer GetTimer(string name, ChunkTimer chunk = null)
        {
            if ((chunk == null || !chunk.Exists()) && !NonChunks.Contains(name)) return null;
            var t = SysDB<CDMTimer>.LoadOrNew("Where Name = @Name AND WorkLoadId = @WorkLoadId AND ChunkId = @ChunkId", new { Name = name, WorkLoadId = workLoad.Id, ChunkId = NonChunks.Contains(name) ? 0 : chunk.ChunkId });
            if (!t.Exists() || Status.COMPLETED != t.Status)
            {
                t.Status = Status.QUEUED;
                t.UpSert();
            }
            return t;
        }
    }
}
