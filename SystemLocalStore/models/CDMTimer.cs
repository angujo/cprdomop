using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLocalStore.models
{
    public class CDMTimer : AbsUpsTable
    {
        public string Name { get; set; }
        public int ChunkId { get; set; }
        public string Query { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Int64 WorkLoadId { get; set; }

        public CDMTimer() { ChunkId = 0; }

        public override string[] UpsColumns()
        {
            return new string[] { "Name", "ChunkId", "WorkLoadId" };
        }
    }
}
