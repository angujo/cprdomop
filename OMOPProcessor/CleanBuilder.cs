using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
