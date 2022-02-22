using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLocalStore.models
{
    public class ChunkTimer : AbsUpsTable
    {
        public int ChunkId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Touched { get; set; }
        public Int64 WorkLoadId { get; set; }

        public static string[] UpsColumns()
        {
            return new string[] { "ChunkId", "WorkLoadId" };
        }
    }
}
