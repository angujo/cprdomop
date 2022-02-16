using DatabaseProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore;

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

        public Task Run()
        {
            return Task.Run(() =>
            {
                List<Action> actions = new List<Action>
                {
                    () => script.CareSite(),
                    () => script.CohortDefinition(),
                    () => script.Location(),
                    () => script.Provider(),
                    () => script.SourceToSource(),
                    () => script.SourceToStandard(),
                };
                Parallel.ForEach(actions, action => action());
            });
        }

        public Task Create() { return Task.Run(() => script.CreateTables()); }
    }
}
