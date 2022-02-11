using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStorage.models
{
    public class WorkLoad
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly ReleaseDate { get; set; }
    }
}
