using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStorage.models
{
    public class SourceFile
    {
        public int Id { get; set; }
        public int WorkLoadId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileHash { get; set; }

    }
}
