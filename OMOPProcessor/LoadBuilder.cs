using DatabaseProcessor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMOPProcessor
{
    internal class LoadBuilder
    {
        Script script;
        AbsDBMSystem dBMSystem;

        public LoadBuilder(Script s)
        {
            script = s;
            dBMSystem = DBMSystem.GetDBMSystem(s.Schema);
        }

        public void Run()
        {
                List<Action> actions = new List<Action>
                {
                    () => script.CdmSource(),
                    () => script.CareSite(),
                    () => script.CohortDefinition(),
                    () => script.Location(),
                    () => script.Provider(),
                    () => script.SourceToSource(),
                    () => script.SourceToStandard(),
                };
                Parallel.ForEach(actions, action => action());
        }

        public void Create() {  script.CreateTables(); }
    }
}
