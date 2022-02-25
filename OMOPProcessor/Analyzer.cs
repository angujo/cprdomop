using System.Collections.Generic;
using System.Reflection;
using SystemLocalStore.models;

namespace OMOPProcessor
{
    internal class Analyzer : AbsCDMQuery
    {
        public Analyzer(DBSchema _schema) : base(_schema) { }
        public List<int> ChunkOrdinals() { return RunColumn<int>(MethodBase.GetCurrentMethod().Name); }
    }
}
