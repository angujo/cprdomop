using DatabaseProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore;

namespace OMOPProcessor
{
    internal class CleanBuilder
    {
        Script script;

        public CleanBuilder(Script s)
        {
            script = s;
        }

        public void Run()
        {
            List<Action> actions = new List<Action>
                {
                    () => script.VisitDetailUpdate(),
                  //  () => script.VisitOccurrenceUpdate(), //VERY HEAVY RUN
                };
            Parallel.ForEach(actions, action => action());
        }
    }
}
