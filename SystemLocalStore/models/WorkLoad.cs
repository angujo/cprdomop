using System;

namespace SystemLocalStore.models
{
    public class WorkLoad : AbsTable
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool FilesLocked { get; set; }
        public bool SourceProcessed { get; set; }

        public WorkLoad() { ReleaseDate = DateTime.Now; }
    }
}
