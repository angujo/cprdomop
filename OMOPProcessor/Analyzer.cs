using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore.models;

namespace OMOPProcessor
{
    internal class Analyzer : AbsCDMQuery
    {
        public Analyzer(DBSchema _schema) : base(_schema) { }
        public List<int> ChunkOrdinals() { return RunColumn<int>(MethodBase.GetCurrentMethod().Name); }
    }
}
