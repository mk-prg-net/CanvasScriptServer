using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace mko.BI.Repositories
{
    static class TraceEnv
    {
        static TraceEnv()
        {
            TraceEnv.Switch = new TraceSwitch("mko_BI_Repositories_TraceSwitch", "TraceSwitch mko.BI.Repositories");
        }

        public static TraceSwitch Switch;

    }
}
