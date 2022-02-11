using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStorage.models
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
