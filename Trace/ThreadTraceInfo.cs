using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trace
{
    internal class ThreadTraceInfo
    {
        internal TraceResult ThreadRootTraceResult { get; private set; }
        internal Stack<TraceResult> startedTraces { get; private set; }

        internal ThreadTraceInfo(TraceResult traceResult)
        {
            ThreadRootTraceResult = traceResult;
            startedTraces = new Stack<TraceResult>();
        }

    }
}
