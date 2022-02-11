using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStorage.models
{
    public class SourceFile:AbsTable
    {
        public Int64 WorkLoadId { get; set; }
        public string TableName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileHash { get; set; }
        public string Code { get; set; }
        public bool IsFile { get; set; }
        public bool Processed { get; set; }

    }
}
